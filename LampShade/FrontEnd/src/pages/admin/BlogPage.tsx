import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, Select, Upload, message, Typography, Image } from 'antd';
import { EditOutlined, PlusOutlined, UploadOutlined } from '@ant-design/icons';
import { articleApi, articleCategoryApi, mediaUrl } from '../../services/api';
import type { ArticleViewModel, ArticleCategory } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const BlogPage: React.FC = () => {
  const [articles, setArticles] = useState<ArticleViewModel[]>([]);
  const [categories, setCategories] = useState<ArticleCategory[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<ArticleViewModel | null>(null);
  const [form] = Form.useForm();

  const fetchArticles = async () => {
    setLoading(true);
    try {
      const data = await articleApi.search({});
      setArticles(data);
    } finally {
      setLoading(false);
    }
  };

  const fetchCategories = async () => {
    try {
      const data = await articleCategoryApi.getAll();
      setCategories(data);
    } catch { /* ignore */ }
  };

  useEffect(() => { fetchArticles(); fetchCategories(); }, []);

  const handleEdit = async (record: ArticleViewModel) => {
    setEditing(record);
    try {
      const details = await articleApi.getDetails(record.id);
      form.setFieldsValue(details);
      setModalOpen(true);
    } catch {
      message.error('Failed to load article details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editing) {
        formData.append('Id', editing.id.toString());
      }
      formData.append('Title', values.title);
      formData.append('Slug', values.slug);
      formData.append('CategoryId', values.categoryId);
      formData.append('ShortDescription', values.shortDescription || '');
      formData.append('Description', values.description || '');
      formData.append('Keywords', values.keywords || '');
      formData.append('MetaDescription', values.metaDescription || '');
      formData.append('PublishDate', values.publishDate || '');
      formData.append('PictureTitle', values.pictureTitle || '');
      formData.append('PictureAlt', values.pictureAlt || '');
      if (values.pictureUrl?.[0]?.originFileObj) {
        formData.append('PictureUrl', values.pictureUrl[0].originFileObj);
      }
      if (editing) {
        await articleApi.edit(formData);
      } else {
        await articleApi.create(formData);
      }
      message.success(editing ? 'Article updated' : 'Article created');
      setModalOpen(false);
      form.resetFields();
      setEditing(null);
      fetchArticles();
    } catch {
      message.error('Failed to save article');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    {
      title: 'Picture', dataIndex: 'pictureUrl', key: 'pictureUrl', width: 80,
      render: (url: string) => url ? <Image src={mediaUrl(url)} width={50} height={50} style={{ objectFit: 'cover' }} /> : null,
    },
    { title: 'Title', dataIndex: 'title', key: 'title' },
    { title: 'Category', dataIndex: 'category', key: 'category' },
    { title: 'Published', dataIndex: 'publishDate', key: 'publishDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: ArticleViewModel) => (
        <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Blog Posts</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => { setEditing(null); form.resetFields(); setModalOpen(true); }}>
          Add Article
        </Button>
      </div>

      <Table columns={columns} dataSource={articles} rowKey="id" loading={loading} />

      <Modal
        title={editing ? 'Edit Article' : 'Create Article'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditing(null); form.resetFields(); }}
        width={700}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="title" label="Title" rules={[{ required: true }]}>
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
          <Form.Item name="publishDate" label="Publish Date">
            <Input placeholder="e.g. 2026/07/01" />
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

export default BlogPage;
