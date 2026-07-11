import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, InputNumber, Input, Select, DatePicker, message, Typography } from 'antd';
import { EditOutlined, PlusOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { discountApi, productApi } from '../../services/api';
import type { CustomerDiscount, ProductViewModel } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const toPersianDate = (date: { toDate: () => Date }) => {
  const parts = new Intl.DateTimeFormat('en-US-u-ca-persian', {
    year: 'numeric', month: '2-digit', day: '2-digit',
  }).formatToParts(date.toDate());
  const value = (type: string) => parts.find(part => part.type === type)?.value ?? '';
  return `${value('year')}/${value('month')}/${value('day')}`;
};

const fromDiscountDate = (storedDate: string, displayDate: string) => {
  const stored = dayjs(storedDate);
  if (stored.isValid() && stored.year() <= 2200) return stored;

  const [year, month, day] = displayDate.split('/').map(Number);
  return dayjs(new Date(year, month - 1, day));
};

const DiscountsPage: React.FC = () => {
  const [discounts, setDiscounts] = useState<CustomerDiscount[]>([]);
  const [products, setProducts] = useState<ProductViewModel[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<CustomerDiscount | null>(null);
  const [form] = Form.useForm();

  const fetchDiscounts = async () => {
    setLoading(true);
    try {
      const data = await discountApi.search({});
      setDiscounts(data);
    } finally {
      setLoading(false);
    }
  };

  const fetchProducts = async () => {
    try {
      const data = await productApi.getAll();
      setProducts(data);
    } catch { /* ignore */ }
  };

  useEffect(() => {
    fetchDiscounts();
    fetchProducts();
  }, []);

  const handleEdit = (record: CustomerDiscount) => {
    setEditing(record);
    form.setFieldsValue({
      productId: record.productId,
      discountRate: record.discountRate,
      startDate: fromDiscountDate(record.startDateGr, record.startDate),
      endDate: fromDiscountDate(record.endDateGr, record.endDate),
      reason: record.reason,
    });
    setModalOpen(true);
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const payload = {
        productId: values.productId,
        discountRate: values.discountRate,
        startDate: toPersianDate(values.startDate),
        endDate: toPersianDate(values.endDate),
        reason: values.reason,
        ...(editing ? { id: editing.id } : {}),
      };
      if (editing) {
        await discountApi.edit(payload);
      } else {
        await discountApi.create(payload);
      }
      message.success(editing ? 'Discount updated' : 'Discount created');
      setModalOpen(false);
      form.resetFields();
      setEditing(null);
      fetchDiscounts();
    } catch {
      message.error('Failed to save discount');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    { title: 'Product', dataIndex: 'product', key: 'product' },
    { title: 'Rate (%)', dataIndex: 'discountRate', key: 'discountRate' },
    { title: 'Start', dataIndex: 'startDate', key: 'startDate' },
    { title: 'End', dataIndex: 'endDate', key: 'endDate' },
    { title: 'Reason', dataIndex: 'reason', key: 'reason' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: CustomerDiscount) => (
        <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Discounts</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => { setEditing(null); form.resetFields(); setModalOpen(true); }}>
          Add Discount
        </Button>
      </div>

      <Table columns={columns} dataSource={discounts} rowKey="id" loading={loading} />

      <Modal
        title={editing ? 'Edit Discount' : 'Create Discount'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditing(null); form.resetFields(); }}
        width={500}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="productId" label="Product" rules={[{ required: true }]}>
            <Select showSearch optionFilterProp="label">
              {products.map(p => <Select.Option key={p.id} value={p.id} label={p.name}>{p.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item name="discountRate" label="Discount Rate (%)" rules={[{ required: true }, { type: 'number', min: 1, max: 99 }]}>
            <InputNumber min={1} max={99} style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="startDate" label="Start Date" rules={[{ required: true }]}>
            <DatePicker format="YYYY/MM/DD" style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="endDate" label="End Date" rules={[{ required: true }]}>
            <DatePicker format="YYYY/MM/DD" style={{ width: '100%' }} />
          </Form.Item>
          <Form.Item name="reason" label="Reason">
            <TextArea rows={2} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default DiscountsPage;
