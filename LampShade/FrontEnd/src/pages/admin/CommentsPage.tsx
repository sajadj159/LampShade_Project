import React, { useEffect, useState } from 'react';
import { Table, Button, Tag, Space, message, Typography, Rate } from 'antd';
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
      setComments(await commentApi.search());
    } catch {
      message.error('Failed to load comments');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchComments(); }, []);

  const changeStatus = async (id: number, action: 'confirm' | 'cancel') => {
    try {
      const result = action === 'confirm' ? await commentApi.confirm(id) : await commentApi.cancel(id);
      if (!result.isSucceeded) {
        message.error(result.message || 'Failed to update comment');
        return;
      }
      message.success(action === 'confirm' ? 'Comment approved' : 'Comment rejected');
      fetchComments();
    } catch {
      message.error('Failed to update comment');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'id', key: 'id', width: 70 },
    { title: 'User', dataIndex: 'name', key: 'name' },
    { title: 'Email', dataIndex: 'email', key: 'email' },
    { title: 'Product ID', dataIndex: 'ownerRecordId', key: 'ownerRecordId' },
    { title: 'Rating', key: 'rating', render: (_: unknown, record: CommentModel) => <Rate disabled value={record.rating} /> },
    { title: 'Comment', dataIndex: 'description', key: 'description', ellipsis: true },
    { title: 'Created', dataIndex: 'commentDate', key: 'commentDate' },
    { title: 'Status', key: 'status', render: (_: unknown, record: CommentModel) => record.isConfirmed ? <Tag color="green">Approved</Tag> : record.isCanceled ? <Tag color="red">Rejected</Tag> : <Tag color="orange">Pending</Tag> },
    { title: 'Actions', key: 'actions', render: (_: unknown, record: CommentModel) => !record.isConfirmed && !record.isCanceled && <Space><Button icon={<CheckOutlined />} onClick={() => changeStatus(record.id, 'confirm')}>Approve</Button><Button danger icon={<CloseOutlined />} onClick={() => changeStatus(record.id, 'cancel')}>Reject</Button></Space> },
  ];

  return <div>
    <Title level={3}>Comment Moderation</Title>
    <Table columns={columns} dataSource={comments} rowKey="id" loading={loading} />
  </div>;
};

export default CommentsPage;