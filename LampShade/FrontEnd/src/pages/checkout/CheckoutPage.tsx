import React, { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import {
  Typography,
  Table,
  Radio,
  Button,
  Card,
  Row,
  Col,
  Space,
  Result,
  Steps,
  message,
} from 'antd';
import { HomeOutlined, CheckCircleOutlined, CreditCardOutlined } from '@ant-design/icons';
import { orderApi } from '../../services/api';
import type { Cart } from '../../types';

const { Title, Text } = Typography;

const CheckoutPage: React.FC = () => {
  const navigate = useNavigate();
  const [cart, setCart] = useState<Cart | null>(null);
  const [paymentMethod, setPaymentMethod] = useState<number>(1);
  const [currentStep, setCurrentStep] = useState(0);
  const [orderId, setOrderId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const stored = localStorage.getItem('computedCart');
    if (stored) {
      setCart(JSON.parse(stored));
    } else {
      navigate('/cart');
    }
  }, [navigate]);

  const handlePlaceOrder = async () => {
    if (!cart) return;

    setLoading(true);
    try {
      const result = await orderApi.place({ ...cart, paymentMethod });
      setOrderId(result.orderId);
      setCurrentStep(1);
      message.success('Order placed successfully!');
      localStorage.removeItem('computedCart');
      localStorage.removeItem('cartItems');
    } catch (error) {
      message.error('Failed to place order');
    } finally {
      setLoading(false);
    }
  };

  if (!cart) {
    return null;
  }

  const columns = [
    {
      title: 'Product',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Price',
      dataIndex: 'unitPrice',
      key: 'unitPrice',
      render: (price: number) => `${price.toLocaleString()} Tomans`,
    },
    {
      title: 'Quantity',
      dataIndex: 'count',
      key: 'count',
    },
    {
      title: 'Total',
      key: 'total',
      render: (_: any, record: any) => `${(record.unitPrice * record.count).toLocaleString()} Tomans`,
    },
  ];

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
        style={{ marginBottom: 48 }}
        items={[
          { title: 'Order Summary' },
          { title: 'Payment' },
          { title: 'Confirmation' },
        ]}
      />

      {currentStep === 0 && (
        <Row gutter={[24, 24]}>
          <Col xs={24} lg={16}>
            <Card title="Order Items">
              <Table
                columns={columns}
                dataSource={cart.items.map((item) => ({ ...item, key: item.id }))}
                pagination={false}
              />
            </Card>

            <Card title="Payment Method" style={{ marginTop: 24 }}>
              <Radio.Group
                value={paymentMethod}
                onChange={(e) => setPaymentMethod(e.target.value)}
                style={{ width: '100%' }}
              >
                <Space direction="vertical" style={{ width: '100%' }}>
                  <Radio value={1}>
                    <Card size="small" hoverable>
                      <Text strong>Online Payment</Text>
                      <br />
                      <Text type="secondary">Pay securely online</Text>
                    </Card>
                  </Radio>
                  <Radio value={2}>
                    <Card size="small" hoverable>
                      <Text strong>Cash on Delivery</Text>
                      <br />
                      <Text type="secondary">Pay when you receive</Text>
                    </Card>
                  </Radio>
                </Space>
              </Radio.Group>
            </Card>
          </Col>

          <Col xs={24} lg={8}>
            <Card title="Order Summary">
              <Space direction="vertical" style={{ width: '100%' }} size="middle">
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                  <Text>Subtotal:</Text>
                  <Text>{cart.totalAmount.toLocaleString()} Tomans</Text>
                </div>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                  <Text type="success">Discount:</Text>
                  <Text type="success">-{cart.discountAmount.toLocaleString()} Tomans</Text>
                </div>
                <div style={{ borderTop: '1px solid #f0f0f0', paddingTop: 16 }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Text strong style={{ fontSize: 18 }}>Total:</Text>
                    <Text strong style={{ fontSize: 18, color: '#ff4d4f' }}>
                      {cart.payAmount.toLocaleString()} Tomans
                    </Text>
                  </div>
                </div>
                <Button
                  type="primary"
                  size="large"
                  block
                  onClick={handlePlaceOrder}
                  loading={loading}
                >
                  Place Order
                </Button>
              </Space>
            </Card>
          </Col>
        </Row>
      )}

      {currentStep === 1 && (
        <Result
          icon={<CheckCircleOutlined style={{ color: '#52c41a' }} />}
          title="Order Placed Successfully!"
          subTitle={`Order ID: ${orderId}`}
          extra={[
            <Button type="primary" key="home" onClick={() => navigate('/')}>
              Go Home
            </Button>,
            <Button key="orders" onClick={() => navigate('/account/orders')}>
              View Orders
            </Button>,
          ]}
        />
      )}
    </div>
  );
};

export default CheckoutPage;
