import React from 'react';
import { Layout, theme } from 'antd';
import { Outlet } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';

const { Content } = Layout;

const MainLayout: React.FC = () => {
  const { token } = theme.useToken();
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header />
      <Content
        style={{
          padding: 0,
          background: token.colorBgLayout,
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
