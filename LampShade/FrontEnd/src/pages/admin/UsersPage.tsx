import React, { useEffect, useState } from 'react';
import { Table, Button, Input, Select, Space, Tag, Modal, Form, message, Typography, Card, Avatar } from 'antd';
import { SearchOutlined, EditOutlined, PlusOutlined } from '@ant-design/icons';
import { accountApi, mediaUrl, roleApi } from '../../services/api';
import type { Account, Role } from '../../types';
import ImageUploadField from '../../components/common/ImageUploadField';

const { Title } = Typography;

const UsersPage: React.FC = () => {
  const [users, setUsers] = useState<Account[]>([]);
  const [roles, setRoles] = useState<Role[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<Account | null>(null);
  const [currentProfilePhoto, setCurrentProfilePhoto] = useState('');
  const [form] = Form.useForm();
  const [searchForm] = Form.useForm();

  const fetchUsers = async (params?: any) => {
    setLoading(true);
    try {
      const data = await accountApi.search(params || {});
      setUsers(data);
    } finally {
      setLoading(false);
    }
  };

  const fetchRoles = async () => {
    try {
      const data = await roleApi.getAll();
      setRoles(data);
    } catch { /* ignore */ }
  };

  useEffect(() => {
    fetchUsers();
    fetchRoles();
  }, []);

  const handleSearch = () => {
    const values = searchForm.getFieldsValue();
    fetchUsers(values);
  };

  const handleCreate = () => {
    setEditingUser(null);
    setCurrentProfilePhoto('');
    form.resetFields();
    setModalOpen(true);
  };

  const handleEdit = async (record: Account) => {
    setEditingUser(record);
    try {
      const details = await accountApi.getDetails(record.id);
      const { profilePhoto, ProfilePhoto, ...accountFields } = details;
      setCurrentProfilePhoto(profilePhoto || ProfilePhoto || '');
      form.setFieldsValue(accountFields);
      setModalOpen(true);
    } catch {
      message.error('Failed to load user details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      const formData = new FormData();
      if (editingUser) {
        formData.append('Id', editingUser.id.toString());
      }
      formData.append('UserName', values.userName);
      formData.append('FullName', values.fullName);
      formData.append('Mobile', values.mobile);
      formData.append('RoleId', values.roleId.toString());
      if (!editingUser) {
        formData.append('Password', values.password);
      }
      formData.append('Address', values.address.trim());
      formData.append('PostalCode', values.postalCode.trim());
      if (values.profilePhoto?.[0]?.originFileObj) {
        formData.append('ProfilePhoto', values.profilePhoto[0].originFileObj);
      }
      if (editingUser) {
        await accountApi.edit(formData);
      } else {
        await accountApi.create(formData);
      }
      message.success(editingUser ? 'User updated' : 'User created');
      setModalOpen(false);
      form.resetFields();
      setEditingUser(null);
      fetchUsers();
    } catch (error) {
      message.error('Failed to save user');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    {
      title: 'Photo', dataIndex: 'profilePhoto', key: 'profilePhoto', width: 72,
      render: (photo: string, record: Account) => <Avatar src={photo ? mediaUrl(photo) : undefined}>{record.fullName?.charAt(0)}</Avatar>,
    },
    { title: 'Full Name', dataIndex: 'fullName', key: 'fullName' },
    { title: 'Username', dataIndex: 'userName', key: 'userName' },
    { title: 'Mobile', dataIndex: 'mobile', key: 'mobile' },
    {
      title: 'Role', dataIndex: 'role', key: 'role',
      render: (role: string) => <Tag color="blue">{role || 'N/A'}</Tag>,
    },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: Account) => (
        <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
      ),
    },
  ];

  return (
    <div>
      <Title level={3}>Users</Title>

      <Card style={{ marginBottom: 16 }}>
        <Form form={searchForm} layout="inline" onFinish={handleSearch}>
          <Form.Item name="fullName"><Input placeholder="Full Name" /></Form.Item>
          <Form.Item name="userName"><Input placeholder="Username" /></Form.Item>
          <Form.Item name="mobile"><Input placeholder="Mobile" /></Form.Item>
          <Form.Item name="roleId">
            <Select placeholder="Role" allowClear style={{ width: 150 }}>
              {roles.map(r => <Select.Option key={r.id} value={r.id}>{r.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item>
            <Space>
              <Button type="primary" icon={<SearchOutlined />} htmlType="submit">Search</Button>
              <Button onClick={() => { searchForm.resetFields(); fetchUsers(); }}>Reset</Button>
            </Space>
          </Form.Item>
        </Form>
      </Card>

      <Button type="primary" icon={<PlusOutlined />} onClick={handleCreate} style={{ marginBottom: 16 }}>Add User</Button>

      <Table columns={columns} dataSource={users} rowKey="id" loading={loading} />

      <Modal
        title={editingUser ? 'Edit User' : 'Create User'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditingUser(null); setCurrentProfilePhoto(''); form.resetFields(); }}
        width={600}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="userName" label="Username" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="fullName" label="Full Name" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="mobile" label="Mobile" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          {!editingUser && (
            <Form.Item name="password" label="Password" rules={[{ required: true, whitespace: true, message: 'Please enter password' }]}>
              <Input.Password />
            </Form.Item>
          )}
          <Form.Item name="roleId" label="Role" rules={[{ required: true }]}>
            <Select>
              {roles.map(r => <Select.Option key={r.id} value={r.id}>{r.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item name="address" label="Address" rules={[{ required: true, whitespace: true, message: 'Please enter address' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="postalCode" label="Postal Code" rules={[{ required: true, whitespace: true, message: 'Please enter postal code' }]}>
            <Input />
          </Form.Item>
          <ImageUploadField currentImage={currentProfilePhoto} name="profilePhoto" label="Profile photo" />
        </Form>
      </Modal>
    </div>
  );
};

export default UsersPage;

