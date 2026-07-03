import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Form, Input, Button, Typography, Card, message } from 'antd';
import { PhoneOutlined, LockOutlined } from '@ant-design/icons';
import { useAuth } from '../../contexts/AuthContext';

const { Title, Text } = Typography;

const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [loading, setLoading] = React.useState(false);

  const onFinish = async (values: { userName: string; password: string }) => {
    setLoading(true);
    try {
      const success = await login(values.userName, values.password);
      if (success) {
        message.success('Login successful!');
        navigate('/');
      } else {
        message.error('Invalid phone number or password');
      }
    } catch {
      message.error('Login failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 'calc(100vh - 64px - 200px)', background: '#f5f5f5' }}>
      <Card style={{ width: 400 }}>
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Title level={3}>Login</Title>
          <Text type="secondary">Sign in with your phone number</Text>
        </div>

        <Form name="login" onFinish={onFinish} layout="vertical">
          <Form.Item name="userName" label="Phone Number" rules={[{ required: true, message: 'Please enter your phone number' }]}>
            <Input prefix={<PhoneOutlined />} placeholder="e.g. 09121234567" size="large" />
          </Form.Item>

          <Form.Item name="password" label="Password" rules={[{ required: true, message: 'Please enter your password' }]}>
            <Input.Password prefix={<LockOutlined />} placeholder="Password" size="large" />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" block size="large" loading={loading}>Login</Button>
          </Form.Item>
        </Form>

        <div style={{ textAlign: 'center' }}>
          <Text type="secondary">Don't have an account? <Link to="/register">Register</Link></Text>
        </div>
      </Card>
    </div>
  );
};

export default LoginPage;
