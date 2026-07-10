import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Layout, Menu, Input, Badge, Avatar, Dropdown, Space, Drawer, Button } from 'antd';
import {
  SearchOutlined,
  ShoppingOutlined,
  UserOutlined,
  MenuOutlined,
  LogoutOutlined,
  ProfileOutlined,
  HistoryOutlined,
  LoginOutlined,
  UserAddOutlined,
  DashboardOutlined,
} from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { useAuth } from '../../contexts/AuthContext';

const { Header: AntHeader } = Layout;
const { Search } = Input;

const Header: React.FC = () => {
  const [drawerOpen, setDrawerOpen] = useState(false);
  const navigate = useNavigate();
  const { isAuthenticated, isAdmin, logout } = useAuth();

  const handleSearch = (value: string) => {
    if (value.trim()) {
      navigate(`/search?q=${encodeURIComponent(value)}`);
    }
  };

  const handleLogout = async () => {
    await logout();
    navigate('/login');
  };

  const userMenuItems: MenuProps['items'] = isAuthenticated
    ? [
        { key: 'profile', icon: <ProfileOutlined />, label: <Link to="/account/profile">Profile</Link> },
        { key: 'orders', icon: <HistoryOutlined />, label: <Link to="/account/orders">Order History</Link> },
        { type: 'divider' },
        { key: 'logout', icon: <LogoutOutlined />, label: 'Logout', onClick: handleLogout },
      ]
    : [
        { key: 'login', icon: <LoginOutlined />, label: <Link to="/login">Login</Link> },
        { key: 'register', icon: <UserAddOutlined />, label: <Link to="/register">Register</Link> },
      ];

  if (isAuthenticated && isAdmin) {
    userMenuItems?.splice(1, 0, { key: 'admin', icon: <DashboardOutlined />, label: <Link to="/admin">Admin Panel</Link> });
  }

  const navMenuItems: MenuProps['items'] = [
    { key: 'home', label: <Link to="/">Home</Link> },
    { key: 'products', label: <Link to="/products">Products</Link> },
    { key: 'blog', label: <Link to="/blog">Blog</Link> },
  ];

  return (
    <>
      <AntHeader style={{ position: 'sticky', top: 0, zIndex: 1000, width: '100%', display: 'flex', alignItems: 'center', padding: '0 24px', background: '#fff', borderBottom: '1px solid #f0f0f0' }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 24, width: '100%' }}>
          <Link to="/" style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
            <span style={{ fontSize: 20, fontWeight: 600, color: '#1677ff' }}>LampShade</span>
          </Link>

          <div className="desktop-nav" style={{ flex: 1, display: 'flex', justifyContent: 'center' }}>
            <Menu mode="horizontal" items={navMenuItems} style={{ border: 'none', flex: 1, justifyContent: 'center', maxWidth: 600 }} />
          </div>

          <Search placeholder="Search products..." onSearch={handleSearch} style={{ maxWidth: 400, flex: 1 }} prefix={<SearchOutlined />} />

          <Space size="middle">
            <Badge count={0} size="small">
              <Link to="/cart">
                <ShoppingOutlined style={{ fontSize: 22, color: '#333' }} />
              </Link>
            </Badge>

            <Dropdown menu={{ items: userMenuItems }} placement="bottomRight">
              <Avatar style={{ backgroundColor: '#1677ff', cursor: 'pointer' }} icon={<UserOutlined />} />
            </Dropdown>

            <Button className="mobile-menu-btn" type="text" icon={<MenuOutlined />} onClick={() => setDrawerOpen(true)} style={{ display: 'none' }} />
          </Space>
        </div>
      </AntHeader>

      <Drawer title="Menu" placement="right" onClose={() => setDrawerOpen(false)} open={drawerOpen} width={280}>
        <Menu mode="inline" items={navMenuItems} onClick={() => setDrawerOpen(false)} />
        <Menu mode="inline" items={userMenuItems} style={{ marginTop: 16 }} onClick={() => setDrawerOpen(false)} />
      </Drawer>
    </>
  );
};

export default Header;
