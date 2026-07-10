import React, { useState, useEffect } from 'react';
import { Card, Typography, Table, Tag, Empty, Spin, Button } from 'antd';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import { orderApi } from '../../services/api';
import type { Order } from '../../types';

const { Title, Text } = Typography;

const OrdersPage: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    try {
      // For demo, we'll try to get paid orders
      // In production, you'd get the current user ID from auth context
      const data = await orderApi.getPaidOrders();
      setOrders(data);
    } catch (error) {
      console.error('Error fetching orders:', error);
    } finally {
      setLoading(false);
    }
  };

  const columns = [
    {
      title: 'Order ID',
      dataIndex: 'id',
      key: 'id',
      render: (id: number) => <Text strong>#{id}</Text>,
    },
    {
      title: 'Payment Method',
      dataIndex: 'paymentMethodId',
      key: 'paymentMethodId',
      render: (methodId: number) => (
        <Tag color={methodId === 1 ? 'blue' : 'green'}>
          {methodId === 1 ? 'Online' : 'Cash'}
        </Tag>
      ),
    },
    {
      title: 'Total Amount',
      dataIndex: 'totalAmount',
      key: 'totalAmount',
      render: (amount: number) => `${amount.toLocaleString()} Tomans`,
    },
    {
      title: 'Discount',
      dataIndex: 'discountAmount',
      key: 'discountAmount',
      render: (amount: number) => (
        <Text type="success">-{amount.toLocaleString()} Tomans</Text>
      ),
    },
    {
      title: 'Pay Amount',
      dataIndex: 'payAmount',
      key: 'payAmount',
      render: (amount: number) => (
        <Text strong style={{ color: '#ff4d4f' }}>
          {amount.toLocaleString()} Tomans
        </Text>
      ),
    },
    {
      title: 'Status',
      dataIndex: 'isPaid',
      key: 'isPaid',
      render: (isPaid: boolean) => (
        <Tag color={isPaid ? 'green' : 'orange'}>
          {isPaid ? 'Paid' : 'Pending'}
        </Tag>
      ),
    },
    {
      title: 'Issue Tracking',
      dataIndex: 'issueTrackingNo',
      key: 'issueTrackingNo',
      render: (tracking: string) => tracking || '-',
    },
    {
      title: 'Pay Date',
      dataIndex: 'payDate',
      key: 'payDate',
      render: (date: string) => date || '-',
    },
  ];

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Card>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}>
          <Title level={3} style={{ margin: 0 }}>
            <ShoppingCartOutlined /> My Orders
          </Title>
          <Link to="/products">
            <Button type="primary">Continue Shopping</Button>
          </Link>
        </div>

        {loading ? (
          <div style={{ display: 'flex', justifyContent: 'center', padding: '48px 0' }}>
            <Spin size="large" />
          </div>
        ) : orders.length === 0 ? (
          <Empty
            description="You have no orders yet"
            style={{ padding: '48px 0' }}
          >
            <Link to="/products">
              <Button type="primary">Start Shopping</Button>
            </Link>
          </Empty>
        ) : (
          <Table
            columns={columns}
            dataSource={orders.map((order) => ({ ...order, key: order.id }))}
            pagination={{ pageSize: 10 }}
            scroll={{ x: 800 }}
          />
        )}
      </Card>
    </div>
  );
};

export default OrdersPage;
