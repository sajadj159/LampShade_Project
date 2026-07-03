import React, { useEffect, useState } from 'react';
import { Table, Button, Input, Select, Space, Tag, Modal, Form, Upload, message, Typography, Card } from 'antd';
import { SearchOutlined, EditOutlined, UploadOutlined } from '@ant-design/icons';
import { accountApi, roleApi } from '../../services/api';
import type { Account, Role } from '../../types';

const { Title } = Typography;

const UsersPage: React.FC = () => {
  const [users, setUsers] = useState<Account[]>([]);
  const [roles, setRoles] = useState<Role[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<Account | null>(null);
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

  const handleEdit = async (record: Account) => {
    setEditingUser(record);
    try {
      const details = await accountApi.getDetails(record.id);
      form.setFieldsValue(details);
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
      formData.append('RoleId', values.roleId);
      formData.append('Address', values.address || '');
      formData.append('PostalCode', values.postalCode || '');
      if (values.profilePhoto?.[0]?.originFileObj) {
        formData.append('ProfilePhoto', values.profilePhoto[0].originFileObj);
      }
      await accountApi.edit(formData);
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

      <Table columns={columns} dataSource={users} rowKey="id" loading={loading} />

      <Modal
        title={editingUser ? 'Edit User' : 'Create User'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditingUser(null); form.resetFields(); }}
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
          <Form.Item name="roleId" label="Role" rules={[{ required: true }]}>
            <Select>
              {roles.map(r => <Select.Option key={r.id} value={r.id}>{r.name}</Select.Option>)}
            </Select>
          </Form.Item>
          <Form.Item name="address" label="Address">
            <Input />
          </Form.Item>
          <Form.Item name="postalCode" label="Postal Code">
            <Input />
          </Form.Item>
          <Form.Item name="profilePhoto" label="Profile Photo" valuePropName="fileList">
            <Upload listType="picture" maxCount={1} beforeUpload={() => false}>
              <Button icon={<UploadOutlined />}>Upload Photo</Button>
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default UsersPage;
