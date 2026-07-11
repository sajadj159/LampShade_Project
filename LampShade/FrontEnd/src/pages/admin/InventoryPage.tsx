import React, { useEffect, useState } from 'react';
import { Button, Descriptions, Form, Input, InputNumber, Modal, Select, Space, Table, Tag, Typography, message } from 'antd';
import { EditOutlined, HistoryOutlined, MinusOutlined, PlusOutlined } from '@ant-design/icons';
import { inventoryApi, productApi } from '../../services/api';
import type { Inventory, InventoryOperation, ProductViewModel } from '../../types';

const { Title } = Typography;

const InventoryPage: React.FC = () => {
  const [inventory, setInventory] = useState<Inventory[]>([]);
  const [products, setProducts] = useState<ProductViewModel[]>([]);
  const [loading, setLoading] = useState(false);
  const [inventoryModalOpen, setInventoryModalOpen] = useState(false);
  const [adjustmentModalOpen, setAdjustmentModalOpen] = useState(false);
  const [operationsModalOpen, setOperationsModalOpen] = useState(false);
  const [editing, setEditing] = useState<Inventory | null>(null);
  const [selected, setSelected] = useState<Inventory | null>(null);
  const [adjustmentType, setAdjustmentType] = useState<'increase' | 'reduce'>('increase');
  const [operations, setOperations] = useState<InventoryOperation[]>([]);
  const [form] = Form.useForm();
  const [adjustmentForm] = Form.useForm();

  const loadInventory = async () => {
    setLoading(true);
    try {
      setInventory(await inventoryApi.search());
    } catch {
      message.error('Failed to load inventory');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadInventory();
    productApi.getAll().then(setProducts).catch(() => message.error('Failed to load products'));
  }, []);

  const openCreate = () => {
    setEditing(null);
    form.resetFields();
    setInventoryModalOpen(true);
  };

  const openEdit = (record: Inventory) => {
    setEditing(record);
    form.setFieldsValue({ productId: record.productId, unitPrice: record.unitPrice });
    setInventoryModalOpen(true);
  };

  const saveInventory = async () => {
    try {
      const values = await form.validateFields();
      const result = editing
        ? await inventoryApi.edit({ id: editing.id, ...values })
        : await inventoryApi.create(values);
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to save inventory');
        return;
      }
      message.success(editing ? 'Inventory updated' : 'Inventory created');
      setInventoryModalOpen(false);
      await loadInventory();
    } catch {
      message.error('Please correct the inventory details');
    }
  };

  const openAdjustment = (record: Inventory, type: 'increase' | 'reduce') => {
    setSelected(record);
    setAdjustmentType(type);
    adjustmentForm.resetFields();
    setAdjustmentModalOpen(true);
  };

  const saveAdjustment = async () => {
    if (!selected) return;
    try {
      const values = await adjustmentForm.validateFields();
      const result = adjustmentType === 'increase'
        ? await inventoryApi.increase({ inventoryId: selected.id, ...values })
        : await inventoryApi.reduce({ inventoryId: selected.id, ...values });
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to update stock');
        return;
      }
      message.success(adjustmentType === 'increase' ? 'Stock increased' : 'Stock reduced');
      setAdjustmentModalOpen(false);
      await loadInventory();
    } catch {
      message.error('Please provide a valid quantity and reason');
    }
  };

  const openOperations = async (record: Inventory) => {
    setSelected(record);
    setOperationsModalOpen(true);
    try {
      setOperations(await inventoryApi.operations(record.id));
    } catch {
      message.error('Failed to load inventory activity');
    }
  };

  const columns = [
    { title: 'Product', dataIndex: 'product', key: 'product' },
    { title: 'Unit Price', dataIndex: 'unitPrice', key: 'unitPrice' },
    { title: 'Count', dataIndex: 'currentCount', key: 'currentCount' },
    { title: 'Status', key: 'status', render: (_: unknown, record: Inventory) => <Tag color={record.inStock ? 'green' : 'red'}>{record.inStock ? 'In Stock' : 'Out of Stock'}</Tag> },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions', render: (_: unknown, record: Inventory) => (
        <Space wrap>
          <Button icon={<PlusOutlined />} onClick={() => openAdjustment(record, 'increase')}>Add stock</Button>
          <Button icon={<MinusOutlined />} onClick={() => openAdjustment(record, 'reduce')}>Reduce</Button>
          <Button icon={<EditOutlined />} onClick={() => openEdit(record)} />
          <Button icon={<HistoryOutlined />} onClick={() => openOperations(record)} />
        </Space>
      ),
    },
  ];

  return <div>
    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
      <Title level={3} style={{ margin: 0 }}>Inventory</Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={openCreate}>Add Product Inventory</Button>
    </div>
    <Table columns={columns} dataSource={inventory} rowKey="id" loading={loading} />

    <Modal title={editing ? 'Edit Inventory' : 'Add Product Inventory'} open={inventoryModalOpen} onOk={saveInventory} onCancel={() => setInventoryModalOpen(false)}>
      <Form form={form} layout="vertical">
        <Form.Item name="productId" label="Product" rules={[{ required: true, message: 'Select a product' }]}>
          <Select showSearch optionFilterProp="label" options={products.map(product => ({ value: product.id, label: product.name }))} />
        </Form.Item>
        <Form.Item name="unitPrice" label="Unit Price" rules={[{ required: true, message: 'Enter a unit price' }]}>
          <InputNumber min={1} style={{ width: '100%' }} />
        </Form.Item>
      </Form>
    </Modal>

    <Modal title={adjustmentType === 'increase' ? 'Add Stock' : 'Reduce Stock'} open={adjustmentModalOpen} onOk={saveAdjustment} onCancel={() => setAdjustmentModalOpen(false)}>
      <Form form={adjustmentForm} layout="vertical">
        <Form.Item name="count" label="Quantity" rules={[{ required: true, message: 'Enter a quantity' }]}>
          <InputNumber min={1} precision={0} style={{ width: '100%' }} />
        </Form.Item>
        <Form.Item name="description" label="Reason" rules={[{ required: true, message: 'Enter a reason' }]}>
          <Input.TextArea rows={3} />
        </Form.Item>
      </Form>
    </Modal>

    <Modal title="Inventory Activity" open={operationsModalOpen} footer={null} onCancel={() => setOperationsModalOpen(false)} width={760}>
      {selected && <Descriptions size="small" column={2} style={{ marginBottom: 16 }} items={[{ key: 'product', label: 'Product', children: selected.product }, { key: 'count', label: 'Current count', children: selected.currentCount }]} />}
      <Table size="small" rowKey="id" dataSource={operations} pagination={false} columns={[
        { title: 'Type', key: 'operation', render: (_: unknown, record: InventoryOperation) => <Tag color={record.operation ? 'green' : 'red'}>{record.operation ? 'Added' : 'Reduced'}</Tag> },
        { title: 'Count', dataIndex: 'count', key: 'count' },
        { title: 'Current', dataIndex: 'currentCount', key: 'currentCount' },
        { title: 'By', dataIndex: 'operator', key: 'operator' },
        { title: 'Reason', dataIndex: 'description', key: 'description' },
        { title: 'Date', dataIndex: 'operationDate', key: 'operationDate' },
      ]} />
    </Modal>
  </div>;
};

export default InventoryPage;