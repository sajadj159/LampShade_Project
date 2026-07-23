import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, Select, message, Typography, Image, Steps } from 'antd';
import { EditOutlined, PlusOutlined } from '@ant-design/icons';
import { articleApi, articleCategoryApi, mediaUrl } from '../../services/api';
import ImageUploadField from '../../components/common/ImageUploadField';
import RichTextEditor from '../../components/common/RichTextEditor';
import type { ArticleViewModel, ArticleCategory } from '../../types';

const { Title } = Typography;
const { TextArea } = Input;

const BlogPage: React.FC = () => {
  const [articles, setArticles] = useState<ArticleViewModel[]>([]);
  const [categories, setCategories] = useState<ArticleCategory[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<ArticleViewModel | null>(null);
  const [formStep, setFormStep] = useState(0);
  const [form] = Form.useForm();
  const [currentPicture, setCurrentPicture] = useState('');

  const resetEditor = () => {
    setModalOpen(false);
    setEditing(null);
    setCurrentPicture('');
    setFormStep(0);
    form.resetFields();
  };

  const fetchArticles = async () => {
    setLoading(true);
    try {
      setArticles(await articleApi.search({}));
    } finally {
      setLoading(false);
    }
  };

  const fetchCategories = async () => {
    try {
      setCategories(await articleCategoryApi.getAll());
    } catch { /* Categories remain empty until the next refresh. */ }
  };

  useEffect(() => { fetchArticles(); fetchCategories(); }, []);

  const handleCreate = () => {
    setEditing(null);
    setCurrentPicture('');
    setFormStep(0);
    form.resetFields();
    setModalOpen(true);
  };

  const handleEdit = async (record: ArticleViewModel) => {
    setEditing(record);
    setFormStep(0);
    try {
      const details = await articleApi.getDetails(record.id);
      const { pictureUrl, PictureUrl, picture, Picture, ...formFields } = details;
      setCurrentPicture(pictureUrl || PictureUrl || picture || Picture || '');
      form.setFieldsValue(formFields);
      setModalOpen(true);
    } catch {
      message.error('Failed to load article details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editing) formData.append('Id', editing.id.toString());

      formData.append('Title', values.title);
      formData.append('Slug', values.slug);
      formData.append('CategoryId', values.categoryId.toString());
      formData.append('ShortDescription', values.shortDescription);
      formData.append('Description', values.description);
      formData.append('Keywords', values.keywords || '');
      formData.append('MetaDescription', values.metaDescription || '');
      formData.append('PublishDate', values.publishDate);
      formData.append('PictureTitle', values.pictureTitle || '');
      formData.append('PictureAlt', values.pictureAlt || '');
      if (values.pictureUrl?.[0]?.originFileObj) formData.append('PictureUrl', values.pictureUrl[0].originFileObj);

      const result = editing ? await articleApi.edit(formData) : await articleApi.create(formData);
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to save article');
        return;
      }

      message.success(editing ? 'Article updated' : 'Article created');
      resetEditor();
      fetchArticles();
    } catch {
      message.error('Failed to save article');
    }
  };

  const stepFields = [
    ['title', 'slug', 'categoryId', 'shortDescription', 'publishDate'],
    ['description'],
    [],
  ];

  const nextStep = async () => {
    try {
      await form.validateFields(stepFields[formStep]);
      setFormStep((current) => current + 1);
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
    { title: 'Title', dataIndex: 'title', key: 'title' },
    { title: 'Category', dataIndex: 'category', key: 'category' },
    { title: 'Published', dataIndex: 'publishDate', key: 'publishDate' },
    { title: 'Actions', key: 'actions', render: (_: unknown, record: ArticleViewModel) => <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button> },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Blog Posts</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate}>Add Article</Button>
      </div>

      <Table columns={columns} dataSource={articles} rowKey="id" loading={loading} />

      <Modal
        title={editing ? 'Edit Article' : 'Create Article'}
        open={modalOpen}
        onCancel={resetEditor}
        width={1000}
        footer={[
          <Button key="cancel" onClick={resetEditor}>Cancel</Button>,
          formStep > 0 && <Button key="back" onClick={() => setFormStep((current) => current - 1)}>Back</Button>,
          formStep < 2
            ? <Button key="next" type="primary" onClick={nextStep}>Next</Button>
            : <Button key="save" type="primary" onClick={handleSave}>{editing ? 'Save Changes' : 'Create Article'}</Button>,
        ]}
      >
        <Steps current={formStep} size="small" items={[{ title: 'Basics' }, { title: 'Content' }, { title: 'SEO & Cover' }]} style={{ marginBottom: 28 }} />
        <Form form={form} layout="vertical" preserve>
          {formStep === 0 && <>
            <Form.Item name="title" label="Title" rules={[{ required: true, whitespace: true }]}><Input /></Form.Item>
            <Form.Item name="slug" label="Slug" rules={[{ required: true, whitespace: true }]}><Input /></Form.Item>
            <Form.Item name="categoryId" label="Category" rules={[{ required: true }]}>
              <Select>{categories.map((category) => <Select.Option key={category.id} value={category.id}>{category.name}</Select.Option>)}</Select>
            </Form.Item>
            <Form.Item name="shortDescription" label="Short Description" rules={[{ required: true, whitespace: true }]}><TextArea autoSize={{ minRows: 3, maxRows: 8 }} /></Form.Item>
            <Form.Item name="publishDate" label="Publish Date" rules={[{ required: true, whitespace: true }]} extra="Use the same date format used by your store, for example 1405/04/22."><Input /></Form.Item>
          </>}

          {formStep === 1 && <Form.Item name="description" label="Article Content" rules={[{ required: true, message: 'Article content is required' }]}><RichTextEditor /></Form.Item>}

          {formStep === 2 && <>
            <Form.Item name="keywords" label="Keywords"><Input /></Form.Item>
            <Form.Item name="metaDescription" label="Meta Description"><TextArea autoSize={{ minRows: 2, maxRows: 5 }} /></Form.Item>
            <Form.Item name="pictureTitle" label="Cover Image Title"><Input /></Form.Item>
            <Form.Item name="pictureAlt" label="Cover Image Alt Text"><Input /></Form.Item>
            <ImageUploadField currentImage={currentPicture} name="pictureUrl" label={editing ? 'Replace Cover Image' : 'Cover Image'} />
          </>}
        </Form>
      </Modal>
    </div>
  );
};

export default BlogPage;