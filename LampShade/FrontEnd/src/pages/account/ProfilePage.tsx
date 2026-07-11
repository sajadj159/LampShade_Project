import React from 'react';
import { Card, Typography, Button, Avatar, Space, Divider } from 'antd';
import { UserOutlined, OrderedListOutlined, LogoutOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { mediaUrl } from '../../services/api';

const { Title, Text } = Typography;

const ProfilePage: React.FC = () => {
  const navigate = useNavigate();
  const { user, logout, isAdmin } = useAuth();

  const handleLogout = async () => {
    await logout();
    navigate('/login');
  };

  return (
    <div style={{ padding: '24px', maxWidth: 800, margin: '0 auto' }}>
      <Card>
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Avatar size={80} src={user?.profilePhoto ? mediaUrl(user.profilePhoto) : undefined} icon={<UserOutlined />} style={{ backgroundColor: '#1677ff' }} />
          <Title level={3} style={{ marginTop: 16 }}>My Profile</Title>
        </div>

        <Divider />

        <Space direction="vertical" size="large" style={{ width: '100%' }}>
          <div>
            <Text type="secondary">Username</Text>
            <br />
            <Text strong>{user?.username || 'N/A'}</Text>
          </div>

          <div>
            <Text type="secondary">Full Name</Text>
            <br />
            <Text strong>{user?.fullname || 'N/A'}</Text>
          </div>

          <div>
            <Text type="secondary">Phone</Text>
            <br />
            <Text strong>{user?.mobile || 'N/A'}</Text>
          </div>

          <div>
            <Text type="secondary">Role</Text>
            <br />
            <Text strong>{user?.role || 'User'}</Text>
          </div>

          <Divider />

          <Space wrap>
            <Button icon={<OrderedListOutlined />} onClick={() => navigate('/account/orders')}>Order History</Button>
            {isAdmin && (
              <Button type="primary" onClick={() => navigate('/admin')}>Admin Panel</Button>
            )}
            <Button danger icon={<LogoutOutlined />} onClick={handleLogout}>Logout</Button>
          </Space>
        </Space>
      </Card>
    </div>
  );
};

export default ProfilePage;

