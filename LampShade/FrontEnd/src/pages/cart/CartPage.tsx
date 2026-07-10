import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Table, Button, Typography, InputNumber, Space, Empty, Card, Row, Col, message, Popconfirm } from 'antd';
import { DeleteOutlined, ShoppingCartOutlined, HomeOutlined } from '@ant-design/icons';
import { cartApi, mediaUrl } from '../../services/api';
import type { CartItem, Cart } from '../../types';

const { Title, Text } = Typography;

const CartPage: React.FC = () => {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadCart();
  }, []);

  const loadCart = () => {
    const stored = localStorage.getItem('cartItems');
    if (stored) {
      const items = JSON.parse(stored);
      setCartItems(items);
    }
  };

  const updateQuantity = (id: number, count: number) => {
    const updated = cartItems.map((item) =>
      item.id === id ? { ...item, count } : item
    );
    setCartItems(updated);
    localStorage.setItem('cartItems', JSON.stringify(updated));
  };

  const removeItem = (id: number) => {
    const updated = cartItems.filter((item) => item.id !== id);
    setCartItems(updated);
    localStorage.setItem('cartItems', JSON.stringify(updated));
    message.success('Item removed from cart');
  };

  const computeCart = async () => {
    if (cartItems.length === 0) {
      message.warning('Cart is empty');
      return;
    }

    setLoading(true);
    try {
      const result = await cartApi.compute(cartItems);
      setCart(result);
    } catch (error) {
      message.error('Failed to compute cart');
    } finally {
      setLoading(false);
    }
  };

  const columns = [
    {
      title: 'Product',
      key: 'product',
      render: (_: any, record: CartItem) => (
        <Space>
          <img
            src={mediaUrl(record.pictureUrl)}
            alt={record.name}
            style={{ width: 60, height: 60, objectFit: 'cover', borderRadius: 4 }}
          />
          <Text>{record.name}</Text>
        </Space>
      ),
    },
    {
      title: 'Price',
      dataIndex: 'unitPrice',
      key: 'unitPrice',
      render: (price: number) => <Text>{price.toLocaleString()} Tomans</Text>,
    },
    {
      title: 'Quantity',
      key: 'quantity',
      render: (_: any, record: CartItem) => (
        <InputNumber
          min={1}
          max={100}
          value={record.count}
          onChange={(value) => updateQuantity(record.id, value || 1)}
        />
      ),
    },
    {
      title: 'Total',
      key: 'total',
      render: (_: any, record: CartItem) => (
        <Text strong>{(record.unitPrice * record.count).toLocaleString()} Tomans</Text>
      ),
    },
    {
      title: 'Action',
      key: 'action',
      render: (_: any, record: CartItem) => (
        <Popconfirm
          title="Remove this item?"
          onConfirm={() => removeItem(record.id)}
        >
          <Button type="text" danger icon={<DeleteOutlined />} />
        </Popconfirm>
      ),
    },
  ];

  const handleCheckout = () => {
    if (!cart) {
      message.warning('Please compute your cart first');
      return;
    }
    localStorage.setItem('computedCart', JSON.stringify(cart));
    navigate('/checkout');
  };

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Space style={{ marginBottom: 24 }}>
        <Link to="/"><HomeOutlined /> Home</Link>
        <Text type="secondary">/</Text>
        <ShoppingCartOutlined /> Shopping Cart
      </Space>

      <Title level={2}>Shopping Cart</Title>

      {cartItems.length === 0 ? (
        <Empty
          description="Your cart is empty"
          style={{ padding: '48px 0' }}
        >
          <Link to="/products">
            <Button type="primary">Continue Shopping</Button>
          </Link>
        </Empty>
      ) : (
        <Row gutter={[24, 24]}>
          <Col xs={24} lg={16}>
            <Table
              columns={columns}
              dataSource={cartItems.map((item) => ({ ...item, key: item.id }))}
              pagination={false}
              loading={loading}
            />
          </Col>
          <Col xs={24} lg={8}>
            <Card title="Cart Summary">
              <Space direction="vertical" style={{ width: '100%' }} size="middle">
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                  <Text>Items:</Text>
                  <Text>{cartItems.length}</Text>
                </div>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                  <Text>Total Quantity:</Text>
                  <Text>{cartItems.reduce((sum, item) => sum + item.count, 0)}</Text>
                </div>
                
                <Button
                  type="default"
                  block
                  onClick={computeCart}
                  loading={loading}
                >
                  Compute Cart
                </Button>

                {cart && (
                  <>
                    <div style={{ borderTop: '1px solid #f0f0f0', paddingTop: 16 }}>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text>Total Amount:</Text>
                        <Text>{cart.totalAmount.toLocaleString()} Tomans</Text>
                      </div>
                      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <Text type="success">Discount:</Text>
                        <Text type="success">-{cart.discountAmount.toLocaleString()} Tomans</Text>
                      </div>
                      <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: 8 }}>
                        <Text strong style={{ fontSize: 18 }}>Pay Amount:</Text>
                        <Text strong style={{ fontSize: 18, color: '#ff4d4f' }}>
                          {cart.payAmount.toLocaleString()} Tomans
                        </Text>
                      </div>
                    </div>
                    <Button
                      type="primary"
                      size="large"
                      block
                      onClick={handleCheckout}
                    >
                      Proceed to Checkout
                    </Button>
                  </>
                )}
              </Space>
            </Card>
          </Col>
        </Row>
      )}
    </div>
  );
};

export default CartPage;
