import React from 'react';
import { Layout, Row, Col, Typography, Space } from 'antd';
import { FacebookOutlined, TwitterOutlined, InstagramOutlined, YoutubeOutlined } from '@ant-design/icons';

const { Footer: AntFooter } = Layout;
const { Title, Text } = Typography;

const Footer: React.FC = () => {
  return (
    <AntFooter
      style={{
        background: '#001529',
        color: '#fff',
        padding: '48px 24px 24px',
      }}
    >
      <Row gutter={[32, 32]}>
        <Col xs={24} sm={12} md={8}>
          <Title level={4} style={{ color: '#fff' }}>LampShade</Title>
          <Text style={{ color: 'rgba(255,255,255,0.65)' }}>
            Your trusted online shopping destination for quality products.
          </Text>
        </Col>
        <Col xs={24} sm={12} md={8}>
          <Title level={5} style={{ color: '#fff' }}>Quick Links</Title>
          <Space direction="vertical" size="small">
            <a href="/" style={{ color: 'rgba(255,255,255,0.65)' }}>Home</a>
            <a href="/products" style={{ color: 'rgba(255,255,255,0.65)' }}>Products</a>
            <a href="/categories" style={{ color: 'rgba(255,255,255,0.65)' }}>Categories</a>
          </Space>
        </Col>
        <Col xs={24} sm={12} md={8}>
          <Title level={5} style={{ color: '#fff' }}>Contact</Title>
          <Space direction="vertical" size="small">
            <Text style={{ color: 'rgba(255,255,255,0.65)' }}>support@lampshade.com</Text>
            <Text style={{ color: 'rgba(255,255,255,0.65)' }}>+1 (555) 123-4567</Text>
          </Space>
          <Space style={{ marginTop: 16 }} size="middle">
            <a href="#" style={{ color: '#fff', fontSize: 18 }}><FacebookOutlined /></a>
            <a href="#" style={{ color: '#fff', fontSize: 18 }}><TwitterOutlined /></a>
            <a href="#" style={{ color: '#fff', fontSize: 18 }}><InstagramOutlined /></a>
            <a href="#" style={{ color: '#fff', fontSize: 18 }}><YoutubeOutlined /></a>
          </Space>
        </Col>
      </Row>
      <div
        style={{
          borderTop: '1px solid rgba(255,255,255,0.1)',
          marginTop: 32,
          paddingTop: 16,
          textAlign: 'center',
        }}
      >
        <Text style={{ color: 'rgba(255,255,255,0.45)' }}>
          © {new Date().getFullYear()} LampShade. All rights reserved.
        </Text>
      </div>
    </AntFooter>
  );
};

export default Footer;
