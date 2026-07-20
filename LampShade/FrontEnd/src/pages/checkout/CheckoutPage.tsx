import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {
  Button,
  Card,
  Col,
  Form,
  Input,
  Radio,
  Result,
  Row,
  Space,
  Steps,
  Table,
  Upload,
  Typography,
  message,
} from 'antd';
import { CheckCircleOutlined, CreditCardOutlined, HomeOutlined, UploadOutlined } from '@ant-design/icons';
import { accountApi, orderApi } from '../../services/api';
import { useAuth } from '../../contexts/AuthContext';
import type { Cart } from '../../types';

const { Title, Text } = Typography;

type DeliveryFormValues = {
  address: string;
  postalCode: string;
};

const CheckoutPage: React.FC = () => {
  const navigate = useNavigate();
  const { user, refreshUser } = useAuth();
  const [deliveryForm] = Form.useForm<DeliveryFormValues>();
  const [cart, setCart] = useState<Cart | null>(null);
  const [paymentMethod, setPaymentMethod] = useState(1);
  const [currentStep, setCurrentStep] = useState(0);
  const [orderId, setOrderId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [receiptLoading, setReceiptLoading] = useState(false);

  useEffect(() => {
    const storedCart = localStorage.getItem('computedCart');
    if (storedCart) {
      setCart(JSON.parse(storedCart));
    } else {
      navigate('/cart', { replace: true });
    }
  }, [navigate]);

  useEffect(() => {
    if (user) {
      deliveryForm.setFieldsValue({
        address: user.address || '',
        postalCode: user.postalCode || '',
      });
    }
  }, [deliveryForm, user]);

  const saveDeliveryDetails = async (values: DeliveryFormValues) => {
    setLoading(true);
    try {
      const result = await accountApi.saveAddress({
        address: values.address.trim(),
        postalCode: values.postalCode.trim(),
      });

      if (!result.isSucceeded) {
        message.error(result.message || 'Unable to save delivery details');
        return;
      }

      await refreshUser();
      setCurrentStep(2);
    } catch {
      message.error('Unable to save delivery details');
    } finally {
      setLoading(false);
    }
  };

  const placeOrder = async () => {
    if (!cart) return;

    setLoading(true);
    try {
      const result = await orderApi.place({ ...cart, paymentMethod });
      setOrderId(result.orderId);
      setCurrentStep(3);
      localStorage.removeItem('computedCart');
      localStorage.removeItem('cartItems');
      message.success('Order placed successfully');
    } catch {
      message.error('Unable to place the order. Please check your details and try again.');
    } finally {
      setLoading(false);
    }
  };

  const uploadPaymentProof = async (file: File) => {
    if (!orderId) return false;
    if (!file.type.startsWith('image/') || file.size > 5 * 1024 * 1024) {
      message.error('Upload an image smaller than 5 MB.');
      return false;
    }

    setReceiptLoading(true);
    try {
      const result = await orderApi.uploadPaymentProof(orderId, file);
      if (result.isSucceeded) {
        message.success('Payment proof uploaded for admin review.');
      } else {
        message.error(result.message || 'Unable to upload payment proof.');
      }
    } catch {
      message.error('Unable to upload payment proof.');
    } finally {
      setReceiptLoading(false);
    }
    return false;
  };
  if (!cart) return null;

  const columns = [
    { title: 'Product', dataIndex: 'name', key: 'name' },
    {
      title: 'Price',
      dataIndex: 'unitPrice',
      key: 'unitPrice',
      render: (price: number) => `${price.toLocaleString()} Tomans`,
    },
    { title: 'Quantity', dataIndex: 'count', key: 'count' },
    {
      title: 'Total',
      key: 'total',
      render: (_: unknown, item: { unitPrice: number; count: number }) =>
        `${(item.unitPrice * item.count).toLocaleString()} Tomans`,
    },
  ];

  const totals = (
    <Space direction="vertical" size="middle" style={{ width: '100%' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <Text>Subtotal</Text>
        <Text>{cart.totalAmount.toLocaleString()} Tomans</Text>
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <Text type="success">Discount</Text>
        <Text type="success">-{cart.discountAmount.toLocaleString()} Tomans</Text>
      </div>
      <div style={{ borderTop: '1px solid #f0f0f0', paddingTop: 16, display: 'flex', justifyContent: 'space-between' }}>
        <Text strong>Total</Text>
        <Text strong style={{ fontSize: 18, color: '#ff4d4f' }}>{cart.payAmount.toLocaleString()} Tomans</Text>
      </div>
    </Space>
  );

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Space style={{ marginBottom: 24 }}>
        <Link to="/"><HomeOutlined /> Home</Link>
        <Text type="secondary">/</Text>
        <CreditCardOutlined /> Checkout
      </Space>

      <Title level={2}>Checkout</Title>
      <Steps
        current={currentStep}
        style={{ marginBottom: 32 }}
        items={[
          { title: 'Review' },
          { title: 'Delivery details' },
          { title: 'Payment' },
          { title: 'Confirmation' },
        ]}
      />

      {currentStep === 0 && (
        <Row gutter={[24, 24]}>
          <Col xs={24} lg={16}>
            <Card title="Order items">
              <Table columns={columns} dataSource={cart.items.map((item) => ({ ...item, key: item.id }))} pagination={false} />
            </Card>
          </Col>
          <Col xs={24} lg={8}>
            <Card title="Order summary">
              {totals}
              <Button type="primary" size="large" block style={{ marginTop: 24 }} onClick={() => setCurrentStep(1)}>
                Continue to delivery details
              </Button>
            </Card>
          </Col>
        </Row>
      )}

      {currentStep === 1 && (
        <Row justify="center">
          <Col xs={24} md={16} lg={12}>
            <Card title="Delivery details">
              <Text type="secondary">This address is saved to your account and used to deliver your order.</Text>
              <Form form={deliveryForm} layout="vertical" onFinish={saveDeliveryDetails} style={{ marginTop: 24 }}>
                <Form.Item name="address" label="Address" rules={[{ required: true, whitespace: true, message: 'Enter your delivery address' }]}>
                  <Input.TextArea autoSize={{ minRows: 3, maxRows: 5 }} placeholder="Full delivery address" />
                </Form.Item>
                <Form.Item name="postalCode" label="Postal code" rules={[{ required: true, whitespace: true, message: 'Enter your postal code' }]}>
                  <Input inputMode="numeric" maxLength={10} placeholder="Postal code" />
                </Form.Item>
                <Space>
                  <Button onClick={() => setCurrentStep(0)}>Back</Button>
                  <Button type="primary" htmlType="submit" loading={loading}>Continue to payment</Button>
                </Space>
              </Form>
            </Card>
          </Col>
        </Row>
      )}

      {currentStep === 2 && (
        <Row gutter={[24, 24]}>
          <Col xs={24} lg={16}>
            <Card title="Payment method">
              <Radio.Group value={paymentMethod} onChange={(event) => setPaymentMethod(event.target.value)} style={{ width: '100%' }}>
                <Space direction="vertical" style={{ width: '100%' }}>
                  <Radio value={1}><Text strong>Online payment</Text><br /><Text type="secondary">Pay securely online</Text></Radio>
                  <Radio value={2}><Text strong>Cash on delivery</Text><br /><Text type="secondary">Pay when you receive your order</Text></Radio>
                </Space>
              </Radio.Group>
            </Card>
            <Card title="Delivery address" style={{ marginTop: 24 }}>
              <Text>{user?.address}</Text><br />
              <Text type="secondary">Postal code: {user?.postalCode}</Text>
              <Button type="link" onClick={() => setCurrentStep(1)}>Edit</Button>
            </Card>
          </Col>
          <Col xs={24} lg={8}>
            <Card title="Order summary">
              {totals}
              <Space direction="vertical" style={{ width: '100%', marginTop: 24 }}>
                <Button block onClick={() => setCurrentStep(1)}>Back</Button>
                <Button type="primary" size="large" block onClick={placeOrder} loading={loading}>Place order</Button>
              </Space>
            </Card>
          </Col>
        </Row>
      )}

      {currentStep === 3 && (
        <>
          <Result
            icon={<CheckCircleOutlined style={{ color: '#52c41a' }} />}
            title="Order placed successfully"
            subTitle={`Order ID: ${orderId}`}
            extra={[
              <Button type="primary" key="home" onClick={() => navigate('/')}>Go home</Button>,
              <Button key="orders" onClick={() => navigate('/account/orders')}>View orders</Button>,
            ]}
          />
          {paymentMethod === 1 && (
            <Card title="Upload payment proof" style={{ maxWidth: 560, margin: '0 auto' }}>
              <Text type="secondary">Upload a clear image of your transfer receipt. An administrator will review and approve the order.</Text>
              <Upload accept="image/*" maxCount={1} beforeUpload={uploadPaymentProof} showUploadList={false} style={{ marginTop: 20 }}>
                <Button icon={<UploadOutlined />} loading={receiptLoading}>Choose receipt image</Button>
              </Upload>
            </Card>
          )}
        </>
      )}
    </div>
  );
};

export default CheckoutPage;

