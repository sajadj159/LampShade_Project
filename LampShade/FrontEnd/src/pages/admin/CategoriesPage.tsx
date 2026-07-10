import React, { useEffect, useState } from 'react';
import { Button, Form, Input, Modal, Popconfirm, Space, Table, Typography, Upload, message } from 'antd';
import { DeleteOutlined, EditOutlined, PlusOutlined, UploadOutlined } from '@ant-design/icons';
import { categoryApi } from '../../services/api';
import type { ProductCategoryViewModel } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const CategoriesPage: React.FC = () => {
  const [categories, setCategories] = useState<ProductCategoryViewModel[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingCategory, setEditingCategory] = useState<ProductCategoryViewModel | null>(null);
  const [form] = Form.useForm();

  const fetchCategories = async () => {
    setLoading(true);
    try {
      setCategories(await categoryApi.getAllAdmin());
    } catch {
      message.error('Failed to load categories');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const closeModal = () => {
    setModalOpen(false);
    setEditingCategory(null);
    form.resetFields();
  };

  const openCreate = () => {
    form.resetFields();
    setEditingCategory(null);
    setModalOpen(true);
  };

  const openEdit = async (category: ProductCategoryViewModel) => {
    try {
      const details = await categoryApi.getDetails(category.id);
      setEditingCategory(category);
      form.setFieldsValue(details);
      setModalOpen(true);
    } catch {
      message.error('Failed to load category details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editingCategory) {
        formData.append('Id', editingCategory.id.toString());
      }

      formData.append('Name', values.name.trim());
      formData.append('Description', values.description?.trim() || '');
      formData.append('PictureAlt', values.pictureAlt?.trim() || '');
      formData.append('PictureTitle', values.pictureTitle?.trim() || '');
      formData.append('Keywords', values.keywords.trim());
      formData.append('MetaDescription', values.metaDescription.trim());
      formData.append('Slug', values.slug.trim());

      if (values.pictureUrl?.[0]?.originFileObj) {
        formData.append('PictureUrl', values.pictureUrl[0].originFileObj);
      }

      const result = editingCategory
        ? await categoryApi.edit(formData)
        : await categoryApi.create(formData);

      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to save category');
        return;
      }

      message.success(editingCategory ? 'Category updated' : 'Category created');
      closeModal();
      fetchCategories();
    } catch {
      message.error('Please correct the highlighted fields');
    }
  };

  const handleDelete = async (id: number) => {
    try {
      const result = await categoryApi.remove(id);
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to delete category');
        return;
      }

      message.success('Category deleted');
      fetchCategories();
    } catch {
      message.error('Failed to delete category');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 80 },
    { title: 'Name', dataIndex: 'name', key: 'name' },
    {
      title: 'Actions',
      key: 'actions',
      width: 150,
      render: (_: unknown, category: ProductCategoryViewModel) => (
        <Space>
          <Button aria-label={`Edit ${category.name}`} icon={<EditOutlined />} onClick={() => openEdit(category)} />
          <Popconfirm
            title="Delete category?"
            description="Categories with products cannot be deleted."
            okText="Delete"
            okButtonProps={{ danger: true }}
            onConfirm={() => handleDelete(category.id)}
          >
            <Button danger aria-label={`Delete ${category.name}`} icon={<DeleteOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Space style={{ width: '100%', justifyContent: 'space-between', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Categories</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={openCreate}>Add Category</Button>
      </Space>

      <Table columns={columns} dataSource={categories} rowKey="id" loading={loading} />

      <Modal
        title={editingCategory ? 'Edit Category' : 'Add Category'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={closeModal}
        okText={editingCategory ? 'Save Changes' : 'Create Category'}
        width={680}
      >
        <Form form={form} layout="vertical" preserve={false}>
          <Form.Item name="name" label="Name" rules={[{ required: true, whitespace: true, message: 'Enter a category name' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="slug" label="Slug" rules={[{ required: true, whitespace: true, message: 'Enter a slug' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Description">
            <TextArea rows={3} />
          </Form.Item>
          <Form.Item name="keywords" label="Keywords" rules={[{ required: true, whitespace: true, message: 'Enter keywords' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="metaDescription" label="Meta Description" rules={[{ required: true, whitespace: true, message: 'Enter a meta description' }]}>
            <TextArea rows={2} />
          </Form.Item>
          <Form.Item name="pictureAlt" label="Picture Alt Text">
            <Input />
          </Form.Item>
          <Form.Item name="pictureTitle" label="Picture Title">
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

export default CategoriesPage;
