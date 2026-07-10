import React from 'react';
import { Layout } from 'antd';
import { Outlet } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';

const { Content } = Layout;

const MainLayout: React.FC = () => {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header />
      <Content
        style={{
          padding: 0,
          background: '#f5f5f5',
          minHeight: 'calc(100vh - 64px - 200px)',
        }}
      >
        <Outlet />
      </Content>
      <Footer />
    </Layout>
  );
};

export default MainLayout;
