import React, { useEffect, useState } from 'react';
import { Card, Typography, Table, Tag, Empty, Spin, Button } from 'antd';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import { orderApi } from '../../services/api';
import { useAuth } from '../../contexts/AuthContext';
import type { Order } from '../../types';

const { Title, Text } = Typography;

const getOrderStatus = (order: Order) => {
  if (order.isCanceled) return { label: 'Cancelled', color: 'red' };
  if (order.paymentMethodId === 2 && !order.isPaid) return { label: 'Awaiting approval', color: 'orange' };
  if (order.paymentMethodId === 2) return { label: 'Approved for delivery', color: 'blue' };
  return order.isPaid ? { label: 'Paid and approved', color: 'green' } : { label: 'Awaiting payment', color: 'orange' };
};

const OrdersPage: React.FC = () => {
  const { user } = useAuth();
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!user) return;
    orderApi.getOrdersByAccount(user.id).then(setOrders).catch(() => setOrders([])).finally(() => setLoading(false));
  }, [user]);

  const columns = [
    { title: 'Order', dataIndex: 'id', render: (id: number) => <Text strong>#{id}</Text> },
    { title: 'Payment', dataIndex: 'paymentMethodId', render: (method: number) => <Tag color={method === 1 ? 'blue' : 'purple'}>{method === 1 ? 'Online' : 'Cash on delivery'}</Tag> },
    { title: 'Amount', dataIndex: 'payAmount', render: (amount: number) => <Text strong>{amount.toLocaleString()} Tomans</Text> },
    { title: 'Status', render: (_: unknown, order: Order) => { const status = getOrderStatus(order); return <Tag color={status.color}>{status.label}</Tag>; } },
    { title: 'Tracking', dataIndex: 'issueTrackingNo', render: (tracking: string) => tracking || '-' },
    { title: 'Order date', dataIndex: 'payDate', render: (date: string) => date || '-' },
  ];

  return <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}><Card><div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}><Title level={3} style={{ margin: 0 }}><ShoppingCartOutlined /> My Orders</Title><Link to="/products"><Button type="primary">Continue shopping</Button></Link></div>{loading ? <div style={{ display: 'flex', justifyContent: 'center', padding: '48px 0' }}><Spin size="large" /></div> : !orders.length ? <Empty description="You have no orders yet" style={{ padding: '48px 0' }}><Link to="/products"><Button type="primary">Start shopping</Button></Link></Empty> : <Table columns={columns} dataSource={orders.map((order) => ({ ...order, key: order.id }))} pagination={{ pageSize: 10 }} scroll={{ x: 800 }} />}</Card></div>;
};

export default OrdersPage;
