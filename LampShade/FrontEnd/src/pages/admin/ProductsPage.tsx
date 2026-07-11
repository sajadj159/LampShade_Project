import React, { useEffect, useState } from 'react';
import { Table, Button, Input, Select, Space, Modal, Form, message, Typography, Card, Image, Upload, Tag, Popconfirm, Steps } from 'antd';
import { SearchOutlined, EditOutlined, PlusOutlined, UploadOutlined, DeleteOutlined } from '@ant-design/icons';
import { productApi, productPictureApi, categoryApi, mediaUrl } from '../../services/api';
import ImageUploadField from '../../components/common/ImageUploadField';
import RichTextEditor from '../../components/common/RichTextEditor';
import type { ProductViewModel, ProductCategoryViewModel, ProductGalleryImage } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const ProductsPage: React.FC = () => {
  const [products, setProducts] = useState<ProductViewModel[]>([]);
  const [categories, setCategories] = useState<ProductCategoryViewModel[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<ProductViewModel | null>(null);
  const [form] = Form.useForm();
  const [currentPicture, setCurrentPicture] = useState('');
  const [galleryPictures, setGalleryPictures] = useState<ProductGalleryImage[]>([]);
  const [clearMainPicture, setClearMainPicture] = useState(false);
  const [formStep, setFormStep] = useState(0);
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
      const [details, pictures] = await Promise.all([
        productApi.getDetails(record.id),
        productPictureApi.search(record.id),
      ]);
      const { pictureUrl, PictureUrl, picture, Picture, ...formFields } = details;
      setCurrentPicture(pictureUrl || PictureUrl || picture || Picture || '');
      setGalleryPictures(pictures);
      setClearMainPicture(false);
      setFormStep(0);
      form.setFieldsValue(formFields);
      setModalOpen(true);
    } catch {
      message.error('Failed to load product details');
    }
  };

  const handleCreate = () => {
    setEditingProduct(null);
    form.resetFields();
    setCurrentPicture('');
    setGalleryPictures([]);
    setClearMainPicture(false);
    setFormStep(0);
    setModalOpen(true);
  };

  const removeGalleryPicture = async (id: number) => {
    try {
      const result = await productPictureApi.remove(id);
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to remove image');
        return;
      }
      setGalleryPictures(current => current.map(picture => picture.id === id ? { ...picture, isRemoved: true } : picture));
      message.success('Gallery image removed');
    } catch {
      message.error('Failed to remove image');
    }
  };
  const handleSave = async () => {
    try {
      await form.validateFields(['name', 'code', 'slug', 'categoryId', 'shortDescription']);
      const values = form.getFieldsValue(true);
      const formData = new FormData();
      if (editingProduct) {
        formData.append('Id', editingProduct.id.toString());
        formData.append('ClearMainPicture', clearMainPicture.toString());
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
      for (const image of values.additionalPictures || []) {
        if (image.originFileObj) formData.append('AdditionalPictures', image.originFileObj);
      }
      const result = editingProduct
        ? await productApi.edit(formData)
        : await productApi.create(formData);

      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to save product');
        return;
      }

      message.success(editingProduct ? 'Product updated' : 'Product created');
      setModalOpen(false);
      form.resetFields();
      setEditingProduct(null);
      setCurrentPicture('');
      setGalleryPictures([]);
      setClearMainPicture(false);
      setFormStep(0);
      fetchProducts();
    } catch {
      message.error('Failed to save product');
    }
  };

  const stepFields = [['name', 'code', 'slug', 'categoryId', 'shortDescription'], ['description'], []];

  const nextStep = async () => {
    try {
      await form.validateFields(stepFields[formStep]);
      setFormStep(current => current + 1);
    } catch {
      message.error('Complete the required fields before continuing');
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
        onCancel={() => { setModalOpen(false); setEditingProduct(null); setCurrentPicture(''); setGalleryPictures([]); setClearMainPicture(false); setFormStep(0); form.resetFields(); }}
        width={1000}
        footer={[
          <Button key="cancel" onClick={() => { setModalOpen(false); setEditingProduct(null); setCurrentPicture(''); setGalleryPictures([]); setClearMainPicture(false); setFormStep(0); form.resetFields(); }}>Cancel</Button>,
          formStep > 0 && <Button key="back" onClick={() => setFormStep(current => current - 1)}>Back</Button>,
          formStep < 2
            ? <Button key="next" type="primary" onClick={nextStep}>Next</Button>
            : <Button key="save" type="primary" onClick={handleSave}>{editingProduct ? 'Save Changes' : 'Create Product'}</Button>,
        ]}
      >
        <Steps current={formStep} size="small" items={[{ title: 'Basics' }, { title: 'Description' }, { title: 'SEO & Images' }]} style={{ marginBottom: 28 }} />
        <Form form={form} layout="vertical" preserve>
          {formStep === 0 && <>
            <Form.Item name="name" label="Name" rules={[{ required: true }]}><Input /></Form.Item>
            <Form.Item name="code" label="Code" rules={[{ required: true }]}><Input /></Form.Item>
            <Form.Item name="slug" label="Slug" rules={[{ required: true }]}><Input /></Form.Item>
            <Form.Item name="categoryId" label="Category" rules={[{ required: true }]}>
              <Select>{categories.map(c => <Select.Option key={c.id} value={c.id}>{c.name}</Select.Option>)}</Select>
            </Form.Item>
            <Form.Item name="shortDescription" label="Short Description" rules={[{ required: true }]}><TextArea autoSize={{ minRows: 3, maxRows: 8 }} /></Form.Item>
          </>}

          {formStep === 1 && <Form.Item name="description" label="Full Description"><RichTextEditor /></Form.Item>}

          {formStep === 2 && <>
            {editingProduct && (
              <div style={{ marginBottom: 24, paddingBottom: 20, borderBottom: '1px solid #f0f0f0' }}>
                <Typography.Title level={5} style={{ marginBottom: 12 }}>Current Product Images</Typography.Title>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: 16 }}>
                  <div style={{ width: 140 }}>
                    <Tag color="blue" style={{ marginBottom: 8 }}>Main Picture</Tag>
                    {currentPicture && !clearMainPicture ? <>
                      <Image src={mediaUrl(currentPicture)} width={140} height={112} style={{ objectFit: 'cover', border: '2px solid #1677ff', borderRadius: 6 }} />
                      <Popconfirm title="Remove the main picture?" onConfirm={() => setClearMainPicture(true)}><Button danger size="small" icon={<DeleteOutlined />} style={{ marginTop: 8 }}>Remove</Button></Popconfirm>
                    </> : <div style={{ height: 112, border: '1px dashed #d9d9d9', display: 'grid', placeItems: 'center' }}>No image</div>}
                  </div>
                  {galleryPictures.map((picture) => (
                    <div key={picture.id} style={{ width: 140, opacity: picture.isRemoved ? 0.45 : 1 }}>
                      <Tag color={picture.isRemoved ? 'default' : 'green'} style={{ marginBottom: 8 }}>{picture.isRemoved ? 'Removed' : 'Gallery Image'}</Tag>
                      <Image src={mediaUrl(picture.pictureUrl)} width={140} height={112} style={{ objectFit: 'cover', borderRadius: 6 }} />
                      {!picture.isRemoved && <Popconfirm title="Remove this gallery image?" onConfirm={() => removeGalleryPicture(picture.id)}><Button danger size="small" icon={<DeleteOutlined />} style={{ marginTop: 8 }}>Remove</Button></Popconfirm>}
                    </div>
                  ))}
                </div>
              </div>
            )}
            <Form.Item name="keywords" label="Keywords"><Input /></Form.Item>
            <Form.Item name="metaDescription" label="Meta Description"><Input /></Form.Item>
            <Form.Item name="pictureTitle" label="Picture Title"><Input /></Form.Item>
            <Form.Item name="pictureAlt" label="Picture Alt"><Input /></Form.Item>
            <ImageUploadField currentImage={currentPicture} name="pictureUrl" label="Replace Main Picture" />
            <Form.Item name="additionalPictures" label="Product Gallery Images" getValueFromEvent={(event) => Array.isArray(event) ? event : event?.fileList ?? []} valuePropName="fileList">
              <Upload multiple accept="image/*" beforeUpload={() => false} listType="picture-card" maxCount={8}><Button icon={<UploadOutlined />}>Add Images</Button></Upload>
            </Form.Item>
          </>}
        </Form>
      </Modal>
    </div>
  );
};

export default ProductsPage;

