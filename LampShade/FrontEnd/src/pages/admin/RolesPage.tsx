import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, Checkbox, message, Typography } from 'antd';
import { EditOutlined, PlusOutlined } from '@ant-design/icons';
import { roleApi } from '../../services/api';
import type { Role } from '../../types';

const { Title } = Typography;

const permissionOptions = [
  { value: 1, label: 'Catalog' },
  { value: 2, label: 'Discount' },
  { value: 3, label: 'Inventory' },
  { value: 4, label: 'Orders' },
  { value: 5, label: 'Account' },
  { value: 6, label: 'Comment' },
  { value: 7, label: 'Slide' },
  { value: 8, label: 'Article' },
  { value: 9, label: 'User' },
  { value: 10, label: 'Role' },
];

const RolesPage: React.FC = () => {
  const [roles, setRoles] = useState<Role[]>([]);
  const [loading, setLoading] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Role | null>(null);
  const [form] = Form.useForm();

  const fetchRoles = async () => {
    setLoading(true);
    try {
      const data = await roleApi.getAll();
      setRoles(data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchRoles(); }, []);

  const handleEdit = async (record: Role) => {
    setEditing(record);
    try {
      const details = await roleApi.getDetails(record.id);
      form.setFieldsValue({
        name: details.name,
        permissions: details.mappedPermissions?.map((p: any) => p.code) || [],
      });
      setModalOpen(true);
    } catch {
      message.error('Failed to load role details');
    }
  };

  const handleSave = async () => {
    try {
      const values = await form.validateFields();
      if (editing) {
        await roleApi.edit({ id: editing.id, name: values.name, permissions: values.permissions || [] });
      } else {
        await roleApi.create({ name: values.name, permissions: values.permissions || [] });
      }
      message.success(editing ? 'Role updated' : 'Role created');
      setModalOpen(false);
      form.resetFields();
      setEditing(null);
      fetchRoles();
    } catch {
      message.error('Failed to save role');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    { title: 'Name', dataIndex: 'name', key: 'name' },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: Role) => (
        <Button icon={<EditOutlined />} onClick={() => handleEdit(record)}>Edit</Button>
      ),
    },
  ];

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 16 }}>
        <Title level={3} style={{ margin: 0 }}>Roles</Title>
        <Button type="primary" icon={<PlusOutlined />} onClick={() => { setEditing(null); form.resetFields(); setModalOpen(true); }}>
          Add Role
        </Button>
      </div>

      <Table columns={columns} dataSource={roles} rowKey="id" loading={loading} />

      <Modal
        title={editing ? 'Edit Role' : 'Create Role'}
        open={modalOpen}
        onOk={handleSave}
        onCancel={() => { setModalOpen(false); setEditing(null); form.resetFields(); }}
        width={500}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="name" label="Role Name" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="permissions" label="Permissions">
            <Checkbox.Group options={permissionOptions} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default RolesPage;
