import React, { useEffect, useState } from 'react';
import { Table, Button, Space, Modal, Form, Input, Tag, message, Typography, Image } from 'antd';
import { EditOutlined, PlusOutlined, DeleteOutlined, UndoOutlined } from '@ant-design/icons';
import { slideApi, mediaUrl } from '../../services/api';
import ImageUploadField from '../../components/common/ImageUploadField';
import type { Slide } from '../../types';

const { Title } = Typography;

const SlidesPage: React.FC = () => {
  const [slides, setSlides] = useState<Slide[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Slide | null>(null);
  const [form] = Form.useForm();
  const [currentPicture, setCurrentPicture] = useState('');

  const fetchSlides = async () => {
    setLoading(true);
    try {
      const data = await slideApi.getAll();
      setSlides(data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchSlides(); }, []);

  const handleEdit = async (record: Slide) => {
    setEditing(record);
    try {
      const details = await slideApi.getDetails(record.id);
      const { pictureUrl, ...formFields } = details;
      setCurrentPicture(pictureUrl || '');
      form.setFieldsValue(formFields);
      setModalOpen(true);
    } catch {
      message.error('Failed to load slide details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editing) {
        formData.append('Id', editing.id.toString());
      }
      formData.append('Heading', values.heading);
      formData.append('Title', values.title);
      formData.append('Text', values.text);
      formData.append('Link', values.link);
      formData.append('BtnText', values.btnText);
      formData.append('PictureTitle', values.pictureTitle || '');
      formData.append('PictureAlt', values.pictureAlt || '');
      if (values.pictureUrl?.[0]?.originFileObj) {
        formData.append('PictureUrl', values.pictureUrl[0].originFileObj);
      }
      const result = editing
        ? await slideApi.edit(formData)
        : await slideApi.create(formData);

      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to save slide');
        return;
      }

      message.success(editing ? 'Slide updated' : 'Slide created');
      setModalOpen(false);
      form.resetFields();
      setEditing(null);
      setCurrentPicture('');
      fetchSlides();
    } catch {
      message.error('Failed to save slide');
    }
  };

  const handleDelete = async (id: number) => {
    Modal.confirm({
      title: 'Delete this slide?',
      onOk: async () => {
        await slideApi.remove(id);
        message.success('Slide deleted');
        fetchSlides();
      },
    });
  };

  const handleRestore = async (id: number) => {
    await slideApi.restore(id);
    message.success('Slide restored');
    fetchSlides();
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    {
      title: 'Picture', dataIndex: 'pictureUrl', key: 'pictureUrl', width: 80,
      render: (url: string) => url ? <Image src={mediaUrl(url)} width={50} height={50} style={{ objectFit: 'cover' }} /> : null,
    },
    { title: 'Heading', dataIndex: 'heading', key: 'heading' },
    { title: 'Title', dataIndex: 'title', key: 'title' },
    {
      title: 'Status', dataIndex: 'isRemoved', key: 'isRemoved',
      render: (removed: boolean) => <Tag color={removed ? 'red' : 'green'}>{removed ? 'Removed' : 'Active'}</Tag>,
    },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: Slide) => (
        <Space>
          <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
          {!record.isRemoved ? (
            <Button danger icon={<DeleteOutlined />} onClick={() => handleDelete(record.id)}>Delete</Button>
          ) : (
            <Button icon={<UndoOutlined />} onClick={() => handleRestore(record.id)}>Restore</Button>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Slides</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => { setEditing(null); setCurrentPicture(''); form.resetFields(); setModalOpen(true); }}>
          Add Slide
        </Button>
      </div>

      <Table columns={columns} dataSource={slides} rowKey="id" loading={loading} />

      <Modal
        title={editing ? 'Edit Slide' : 'Create Slide'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditing(null); setCurrentPicture(''); form.resetFields(); }}
        width={600}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="heading" label="Heading" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="title" label="Title" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="text" label="Text" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="link" label="Link" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="btnText" label="Button Text" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="pictureTitle" label="Picture Title">
            <Input />
          </Form.Item>
          <Form.Item name="pictureAlt" label="Picture Alt">
            <Input />
          </Form.Item>
          <ImageUploadField currentImage={currentPicture} name="pictureUrl" label="Picture" />
        </Form>
      </Modal>
    </div>
  );
};

export default SlidesPage;

