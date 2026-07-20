import React, { useEffect, useState } from 'react';
import { Button, Card, Image, message, Popconfirm, Table, Tag, Typography } from 'antd';
import { CheckOutlined, ReloadOutlined } from '@ant-design/icons';
import { mediaUrl, orderApi } from '../../services/api';
import type { Order } from '../../types';

const { Title, Text } = Typography;

const getOrderStatus = (order: Order) => {
  if (order.isCanceled) return { label: 'Cancelled', color: 'red' };
  if (order.paymentMethodId === 2 && !order.isPaid) return { label: 'Awaiting approval', color: 'orange' };
  if (order.paymentMethodId === 2) return { label: 'Approved for delivery', color: 'blue' };
  if (!order.isPaid && order.paymentProofUrl) return { label: 'Receipt ready for review', color: 'gold' };
  return order.isPaid ? { label: 'Paid and approved', color: 'green' } : { label: 'Awaiting payment proof', color: 'orange' };
};

const AdminOrdersPage: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(false);
  const [approvingId, setApprovingId] = useState<number | null>(null);

  const loadOrders = async () => {
    setLoading(true);
    try {
      setOrders(await orderApi.search({ isCanceled: false }));
    } catch {
      message.error('Unable to load orders');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { loadOrders(); }, []);

  const approve = async (order: Order) => {
    setApprovingId(order.id);
    try {
      if (order.paymentMethodId === 2) {
        await orderApi.approveCashOnDelivery(order.id);
      } else {
        await orderApi.approvePaymentProof(order.id);
      }
      message.success('Order approved');
      await loadOrders();
    } catch {
      message.error('This order cannot be approved');
    } finally {
      setApprovingId(null);
    }
  };

  const canApprove = (order: Order) => !order.isCanceled && !order.isPaid && (order.paymentMethodId === 2 || Boolean(order.paymentProofUrl));

  const columns = [
    { title: 'Order', dataIndex: 'id', render: (id: number) => <Text strong>#{id}</Text> },
    { title: 'Customer', dataIndex: 'accountFullName', render: (name: string) => name || '-' },
    { title: 'Payment', dataIndex: 'paymentMethodId', render: (method: number) => <Tag color={method === 2 ? 'purple' : 'blue'}>{method === 2 ? 'Cash on delivery' : 'Online'}</Tag> },
    { title: 'Amount', dataIndex: 'payAmount', render: (amount: number) => <Text strong>{amount.toLocaleString()} Tomans</Text> },
    { title: 'Status', render: (_: unknown, order: Order) => { const status = getOrderStatus(order); return <Tag color={status.color}>{status.label}</Tag>; } },
    { title: 'Date', dataIndex: 'creationDate', render: (date: string) => date || '-' },
    { title: 'Receipt', render: (_: unknown, order: Order) => order.paymentProofUrl ? <Image src={mediaUrl(order.paymentProofUrl)} alt={'Payment proof for order ' + order.id} width={44} height={44} style={{ objectFit: 'cover', borderRadius: 4 }} /> : <Text type="secondary">-</Text> },
    { title: 'Action', render: (_: unknown, order: Order) => canApprove(order) ? <Popconfirm title="Approve this order? Inventory will be reserved." onConfirm={() => approve(order)}><Button type="primary" icon={<CheckOutlined />} loading={approvingId === order.id}>Approve</Button></Popconfirm> : <Text type="secondary">No action needed</Text> },
  ];

  return <Card><div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24 }}><Title level={3} style={{ margin: 0 }}>Orders</Title><Button icon={<ReloadOutlined />} onClick={loadOrders} loading={loading}>Refresh</Button></div><Table columns={columns} dataSource={orders.map((order) => ({ ...order, key: order.id }))} loading={loading} pagination={{ pageSize: 10 }} scroll={{ x: 1080 }} /></Card>;
};

export default AdminOrdersPage;
