import React, { useState } from 'react';
import { Outlet, useNavigate, useLocation } from 'react-router-dom';
import { Layout, Menu, Avatar, Dropdown, Space, Typography, theme, Button } from 'antd';
import {
  DashboardOutlined,
  UserOutlined,
  ShoppingOutlined,
  ShoppingCartOutlined,
  PercentageOutlined,
  PictureOutlined,
  SafetyCertificateOutlined,
  ReadOutlined,
  LogoutOutlined,
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  CommentOutlined,
  TagsOutlined,
  AppstoreOutlined,
  InboxOutlined,
  MoonOutlined,
  SunOutlined,
} from '@ant-design/icons';
import { useAuth } from '../../contexts/AuthContext';
import { mediaUrl } from '../../services/api';
import { useThemeMode } from '../../contexts/ThemeModeContext';
import { useLanguage } from '../../contexts/LanguageContext';
import LanguageToggle from '../common/LanguageToggle';
import type { MenuProps } from 'antd';

const { Header, Sider, Content } = Layout;
const { Text } = Typography;

const AdminLayout: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout } = useAuth();
  const { isDark, toggleTheme } = useThemeMode();
  const { token: themeToken } = theme.useToken();
  const { t } = useLanguage();

  const menuItems: MenuProps['items'] = [
    {
      key: '/admin',
      icon: <DashboardOutlined />,
      label: t('dashboard'),
    },
    {
      key: '/admin/users',
      icon: <UserOutlined />,
      label: t('users'),
    },
    {
      key: '/admin/products',
      icon: <ShoppingOutlined />,
      label: t('products'),
    },
    {
      key: '/admin/categories',
      icon: <AppstoreOutlined />,
      label: t('categories'),
    },
    {
      key: '/admin/inventory',
      icon: <InboxOutlined />,
      label: t('inventory'),
    },    {
      key: '/admin/orders',
      icon: <ShoppingCartOutlined />,
      label: 'Orders',
    },    {
      key: '/admin/discounts',
      icon: <PercentageOutlined />,
      label: t('discounts'),
    },
    {
      key: '/admin/slides',
      icon: <PictureOutlined />,
      label: t('slides'),
    },
    {
      key: '/admin/roles',
      icon: <SafetyCertificateOutlined />,
      label: t('roles'),
    },
    {
      key: '/admin/blog',
      icon: <ReadOutlined />,
      label: t('blogPosts'),
    },
    {
      key: '/admin/comments',
      icon: <CommentOutlined />,
      label: t('comments'),
    },
  ];

  const handleLogout = async () => {
    await logout();
    navigate('/login');
  };

  const userMenuItems: MenuProps['items'] = [
    {
      key: 'profile',
      icon: <UserOutlined />,
      label: t('profile'),
      onClick: () => navigate('/account/profile'),
    },
    {
      key: 'shop',
      icon: <TagsOutlined />,
      label: 'Go to Shop',
      onClick: () => navigate('/'),
    },
    { type: 'divider' },
    {
      key: 'logout',
      icon: <LogoutOutlined />,
      label: t('logout'),
      onClick: handleLogout,
    },
  ];

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider
        trigger={null}
        collapsible
        collapsed={collapsed}
        breakpoint="lg"
        onBreakpoint={(broken) => setCollapsed(broken)}
        style={{ overflow: 'auto', height: '100vh', position: 'sticky', top: 0, left: 0 }}
      >
        <div style={{
          height: 64,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          color: '#fff',
          fontSize: collapsed ? 16 : 20,
          fontWeight: 700,
          borderBottom: '1px solid rgba(255,255,255,0.1)',
        }}>
          {collapsed ? 'LS' : 'LampShade Admin'}
        </div>
        <Menu
          theme="dark"
          mode="inline"
          selectedKeys={[location.pathname]}
          items={menuItems}
          onClick={({ key }) => navigate(key)}
        />
      </Sider>

      <Layout>
        <Header style={{
          padding: '0 24px',
          background: themeToken.colorBgContainer,
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          boxShadow: '0 1px 4px rgba(0,0,0,0.08)',
          position: 'sticky',
          top: 0,
          zIndex: 10,
        }}>
          <Space>
            {React.createElement(collapsed ? MenuUnfoldOutlined : MenuFoldOutlined, {
              className: 'trigger',
              onClick: () => setCollapsed(!collapsed),
              style: { fontSize: 18, cursor: 'pointer' },
            })}
          </Space>

<LanguageToggle />
          <Button type="text" icon={isDark ? <SunOutlined /> : <MoonOutlined />} onClick={toggleTheme} />          <Dropdown menu={{ items: userMenuItems }} placement="bottomRight">
            <Space style={{ cursor: 'pointer' }}>
              <Avatar src={user?.profilePhoto ? mediaUrl(user.profilePhoto) : undefined} icon={<UserOutlined />} style={{ backgroundColor: '#1677ff' }} />
              <Text>{user?.fullname || user?.username || 'Admin'}</Text>
            </Space>
          </Dropdown>
        </Header>

        <Content className="admin-content" style={{ margin: 24, padding: 24, background: themeToken.colorBgContainer, borderRadius: 8, minHeight: 280 }}>
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
};

export default AdminLayout;




