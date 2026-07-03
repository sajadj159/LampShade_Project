import React, { useEffect, useState } from 'react';
import { Table, Button, Tag, Space, message, Typography } from 'antd';
import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import { commentApi } from '../../services/api';
import type { CommentModel } from '../../types';

const { Title } = Typography;

const CommentsPage: React.FC = () => {
  const [comments, setComments] = useState<CommentModel[]>([]);
  const [loading, setLoading] = useState(false);

  const fetchComments = async () => {
    setLoading(true);
    try {
      const data = await commentApi.getAll();
      setComments(data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchComments(); }, []);

  const handleConfirm = async (id: number) => {
    await commentApi.confirm(id);
    message.success('Comment confirmed');
    fetchComments();
  };

  const handleCancel = async (id: number) => {
    await commentApi.cancel(id);
    message.success('Comment cancelled');
    fetchComments();
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 60 },
    { title: 'Name', dataIndex: 'name', key: 'name' },
    { title: 'Email', dataIndex: 'email', key: 'email' },
    { title: 'Description', dataIndex: 'description', key: 'description', ellipsis: true },
    {
      title: 'Status', key: 'status',
      render: (_: any, record: CommentModel) => (
        <Space>
          {record.isConfirmed && <Tag color="green">Confirmed</Tag>}
          {record.isCanceled && <Tag color="red">Cancelled</Tag>}
          {!record.isConfirmed && !record.isCanceled && <Tag color="orange">Pending</Tag>}
        </Space>
      ),
    },
    { title: 'Created', dataIndex: 'creationDate', key: 'creationDate' },
    {
      title: 'Actions', key: 'actions',
      render: (_: any, record: CommentModel) => (
        <Space>
          {!record.isConfirmed && !record.isCanceled && (
            <>
              <Button icon={<CheckOutlined />} onClick={() => handleConfirm(record.id)}>Confirm</Button>
              <Button danger icon={<CloseOutlined />} onClick={() => handleCancel(record.id)}>Cancel</Button>
            </>
          )}
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Title level={3}>Comments</Title>
      <Table columns={columns} dataSource={comments} rowKey="id" loading={loading} />
    </div>
  );
};

export default CommentsPage;
