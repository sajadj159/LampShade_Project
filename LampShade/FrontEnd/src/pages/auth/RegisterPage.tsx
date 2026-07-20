import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Form, Input, Button, Typography, Card, message } from 'antd';
import { PhoneOutlined, UserOutlined, LockOutlined, HomeOutlined } from '@ant-design/icons';
import { authApi } from '../../services/api';
import ImageUploadField from '../../components/common/ImageUploadField';

const { Title, Text } = Typography;

const RegisterPage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const returnTo = (location.state as { from?: string } | null)?.from || '/';
  const [loading, setLoading] = React.useState(false);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append('UserName', values.userName.trim());
      formData.append('FullName', values.fullName.trim());
      formData.append('Password', values.password);
      formData.append('Mobile', values.mobile.trim());
      formData.append('Address', values.address.trim());
      formData.append('PostalCode', values.postalCode.trim());
      formData.append('RoleId', '2');

      if (values.profilePhoto?.[0]?.originFileObj) {
        formData.append('profilePhoto', values.profilePhoto[0].originFileObj);
      }

      const result = await authApi.register(formData);
      if (result.isSucceeded) {
        message.success('Registration successful! You can now login.');
        navigate('/login', { state: { from: returnTo } });
      } else {
        message.error(result.message || 'Registration failed');
      }
    } catch {
      message.error('Registration failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 'calc(100vh - 64px - 200px)', background: '#f5f5f5', padding: '24px 0' }}>
      <Card style={{ width: 500 }}>
        <div style={{ textAlign: 'center', marginBottom: 24 }}>
          <Title level={3}>Register</Title>
          <Text type="secondary">Create an account with your phone number</Text>
        </div>

        <Form name="register" onFinish={onFinish} layout="vertical">
          <Form.Item
            name="userName"
            label="Username"
            rules={[{ required: true, whitespace: true, message: 'Please enter a username' }]}
          >
            <Input prefix={<UserOutlined />} placeholder="Username" size="large" />
          </Form.Item>
          <Form.Item name="mobile" label="Phone Number" rules={[
            { required: true, whitespace: true, message: 'Please enter your phone number' },
            { pattern: /^09\d{9}$/, message: 'Please enter a valid Iranian phone number (09XXXXXXXXX)' },
          ]}>
            <Input prefix={<PhoneOutlined />} placeholder="e.g. 09121234567" size="large" />
          </Form.Item>

          <Form.Item name="fullName" label="Full Name" rules={[{ required: true, whitespace: true, message: 'Please enter your full name' }]}>
            <Input prefix={<UserOutlined />} placeholder="Full Name" size="large" />
          </Form.Item>

          <Form.Item name="password" label="Password" rules={[
            { required: true, message: 'Please enter a password' },
            { min: 6, message: 'Password must be at least 6 characters' },
          ]}>
            <Input.Password prefix={<LockOutlined />} placeholder="Password" size="large" />
          </Form.Item>

          <Form.Item name="address" label="Address" rules={[{ required: true, whitespace: true, message: 'Please enter your address' }]}>
            <Input prefix={<HomeOutlined />} placeholder="Address" size="large" />
          </Form.Item>

          <Form.Item name="postalCode" label="Postal Code" rules={[{ required: true, whitespace: true, message: 'Please enter your postal code' }]}>
            <Input placeholder="Postal Code" size="large" />
          </Form.Item>

          <ImageUploadField name="profilePhoto" label="Profile photo" />

          <Form.Item>
            <Button type="primary" htmlType="submit" block size="large" loading={loading}>Register</Button>
          </Form.Item>
        </Form>

        <div style={{ textAlign: 'center' }}>
          <Text type="secondary">Already have an account? <Link to="/login" state={{ from: returnTo }}>Login</Link></Text>
        </div>
      </Card>
    </div>
  );
};

export default RegisterPage;

