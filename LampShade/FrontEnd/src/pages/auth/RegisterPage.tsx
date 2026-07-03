import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Form, Input, Button, Typography, Card, Upload, message } from 'antd';
import { PhoneOutlined, UserOutlined, LockOutlined, HomeOutlined, UploadOutlined } from '@ant-design/icons';
import { authApi } from '../../services/api';

const { Title, Text } = Typography;

const RegisterPage: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = React.useState(false);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const formData = new FormData();
      formData.append('userName', values.mobile);
      formData.append('fullName', values.fullName);
      formData.append('password', values.password);
      formData.append('mobile', values.mobile);
      formData.append('address', values.address || '');
      formData.append('postalCode', values.postalCode || '');
      formData.append('roleId', '2');

      if (values.profilePhoto?.[0]?.originFileObj) {
        formData.append('profilePhoto', values.profilePhoto[0].originFileObj);
      }

      const result = await authApi.register(formData);
      if (result.isSucceeded) {
        message.success('Registration successful! You can now login.');
        navigate('/login');
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
          <Form.Item name="mobile" label="Phone Number" rules={[
            { required: true, message: 'Please enter your phone number' },
            { pattern: /^09\d{9}$/, message: 'Please enter a valid Iranian phone number (09XXXXXXXXX)' },
          ]}>
            <Input prefix={<PhoneOutlined />} placeholder="e.g. 09121234567" size="large" />
          </Form.Item>

          <Form.Item name="fullName" label="Full Name" rules={[{ required: true, message: 'Please enter your full name' }]}>
            <Input prefix={<UserOutlined />} placeholder="Full Name" size="large" />
          </Form.Item>

          <Form.Item name="password" label="Password" rules={[
            { required: true, message: 'Please enter a password' },
            { min: 6, message: 'Password must be at least 6 characters' },
          ]}>
            <Input.Password prefix={<LockOutlined />} placeholder="Password" size="large" />
          </Form.Item>

          <Form.Item name="address" label="Address">
            <Input prefix={<HomeOutlined />} placeholder="Address (optional)" size="large" />
          </Form.Item>

          <Form.Item name="postalCode" label="Postal Code">
            <Input placeholder="Postal Code (optional)" size="large" />
          </Form.Item>

          <Form.Item name="profilePhoto" label="Profile Photo" valuePropName="fileList">
            <Upload listType="picture" maxCount={1} beforeUpload={() => false}>
              <Button icon={<UploadOutlined />}>Upload Photo</Button>
            </Upload>
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" block size="large" loading={loading}>Register</Button>
          </Form.Item>
        </Form>

        <div style={{ textAlign: 'center' }}>
          <Text type="secondary">Already have an account? <Link to="/login">Login</Link></Text>
        </div>
      </Card>
    </div>
  );
};

export default RegisterPage;
