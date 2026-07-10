import React, { useEffect, useState } from 'react';
import { Table, Button, Input, Select, Space, Modal, Form, Upload, message, Typography, Card, Image } from 'antd';
import { SearchOutlined, EditOutlined, PlusOutlined, UploadOutlined } from '@ant-design/icons';
import { productApi, categoryApi, mediaUrl } from '../../services/api';
import type { ProductViewModel, ProductCategoryViewModel } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const ProductsPage: React.FC = () => {
  const [products, setProducts] = useState<ProductViewModel[]>([]);
  const [categories, setCategories] = useState<ProductCategoryViewModel[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<ProductViewModel | null>(null);
  const [form] = Form.useForm();
  const [searchForm] = Form.useForm();

  const fetchProducts = async (params?: any) => {
    setLoading(true);
    try {
      const data = params ? await productApi.searchAdmin(params) : await productApi.getAll();
      setProducts(data);
    } finally {
      setLoading(false);
    }
  };

  const fetchCategories = async () => {
    try {
      const data = await categoryApi.getAllAdmin();
      setCategories(data);
    } catch { /* ignore */ }
  };

  useEffect(() => {
    fetchProducts();
    fetchCategories();
  }, []);

  const handleSearch = () => {
    const values = searchForm.getFieldsValue();
    fetchProducts(values);
  };

  const handleEdit = async (record: ProductViewModel) => {
    setEditingProduct(record);
    try {
      const details = await productApi.getDetails(record.id);
      form.setFieldsValue(details);
      setModalOpen(true);
    } catch {
      message.error('Failed to load product details');
    }
  };

  const handleCreate = () => {
    setEditingProduct(null);
    form.resetFields();
    setModalOpen(true);
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editingProduct) {
        formData.append('Id', editingProduct.id.toString());
      }
      formData.append('Name', values.name);
      formData.append('Code', values.code);
      formData.append('Slug', values.slug);
      formData.append('CategoryId', values.categoryId);
      formData.append('ShortDescription', values.shortDescription || '');
      formData.append('Description', values.description || '');
      formData.append('Keywords', values.keywords || '');
      formData.append('MetaDescription', values.metaDescription || '');
      formData.append('PictureTitle', values.pictureTitle || '');
      formData.append('PictureAlt', values.pictureAlt || '');
      if (values.pictureUrl?.[0]?.originFileObj) {
        formData.append('PictureUrl', values.pictureUrl[0].originFileObj);
      }
      if (editingProduct) {
        await productApi.edit(formData);
      } else {
        await productApi.create(formData);
      }
      message.success(editingProduct ? 'Product updated' : 'Product created');
      setModalOpen(false);
      form.resetFields();
      setEditingProduct(null);
      fetchProducts();
    } catch {
      message.error('Failed to save product');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    {
      title: 'Picture', dataIndex: 'pictureUrl', key: 'pictureUrl', width: 80,
      render: (url: string) => url ? <Image src={mediaUrl(url)} width={50} height={50} style={{ objectFit: 'cover' }} /> : null,
    },
    { title: 'Name', dataIndex: 'name', key: 'name' },
    { title: 'Code', dataIndex: 'code', key: 'code' },
    { title: 'Category', dataIndex: 'category', key: 'category' },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: ProductViewModel) => (
        <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Products</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>Add Product</Button>
      </div>

      <Card style={{ marginBottom: 16 }}>
        <Form form={searchForm} layout="inline" onFinish={handleSearch}>
          <Form.Item name="name"><Input placeholder="Product Name" /></Form.Item>
          <Form.Item name="code"><Input placeholder="Product Code" /></Form.Item>
          <Form.Item name="categoryId">
            <Select placeholder="Category" allowClear style={{ width: 180 }}>
              {categories.map(c => <Select.Option key={c.id} value={c.id}>{c.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item>
            <Space>
              <Button type="primary" icon={<SearchOutlined />} htmlType="submit">Search</Button>
              <Button onClick={() => { searchForm.resetFields(); fetchProducts(); }}>Reset</Button>
            </Space>
          </Form.Item>
        </Form>
      </Card>

      <Table columns={columns} dataSource={products} rowKey="id" loading={loading} />

      <Modal
        title={editingProduct ? 'Edit Product' : 'Create Product'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditingProduct(null); form.resetFields(); }}
        width={700}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="name" label="Name" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="code" label="Code" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="slug" label="Slug" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="categoryId" label="Category" rules={[{ required: true }]}>
            <Select>
              {categories.map(c => <Select.Option key={c.id} value={c.id}>{c.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item name="shortDescription" label="Short Description">
            <TextArea rows={2} />
          </Form.Item>
          <Form.Item name="description" label="Description">
            <TextArea rows={4} />
          </Form.Item>
          <Form.Item name="keywords" label="Keywords">
            <Input />
          </Form.Item>
          <Form.Item name="metaDescription" label="Meta Description">
            <Input />
          </Form.Item>
          <Form.Item name="pictureTitle" label="Picture Title">
            <Input />
          </Form.Item>
          <Form.Item name="pictureAlt" label="Picture Alt">
            <Input />
          </Form.Item>
          <Form.Item name="pictureUrl" label="Picture" valuePropName="fileList">
            <Upload listType="picture" maxCount={1} beforeUpload={() => false}>
              <Button icon={<UploadOutlined />}>Upload Picture</Button>
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ProductsPage;
