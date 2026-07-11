import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Layout, Menu, Input, Badge, Avatar, Dropdown, Space, Drawer, Button, Tooltip, theme } from 'antd';
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
  MoonOutlined,
  SunOutlined,
} from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { useAuth } from '../../contexts/AuthContext';
import { mediaUrl } from '../../services/api';
import { useThemeMode } from '../../contexts/ThemeModeContext';
import { useLanguage } from '../../contexts/LanguageContext';
import LanguageToggle from '../common/LanguageToggle';

const { Header: AntHeader } = Layout;
const { Search } = Input;

const Header: React.FC = () => {
  const [drawerOpen, setDrawerOpen] = useState(false);
  const navigate = useNavigate();
  const { isAuthenticated, isAdmin, logout, user } = useAuth();
  const { isDark, toggleTheme } = useThemeMode();
  const { token } = theme.useToken();
  const { t } = useLanguage();

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
        { key: 'profile', icon: <ProfileOutlined />, label: <Link to="/account/profile">{t('profile')}</Link> },
        { key: 'orders', icon: <HistoryOutlined />, label: <Link to="/account/orders">{t('orderHistory')}</Link> },
        { type: 'divider' },
        { key: 'logout', icon: <LogoutOutlined />, label: t('logout'), onClick: handleLogout },
      ]
    : [
        { key: 'login', icon: <LoginOutlined />, label: <Link to="/login">{t('login')}</Link> },
        { key: 'register', icon: <UserAddOutlined />, label: <Link to="/register">{t('register')}</Link> },
      ];

  if (isAuthenticated && isAdmin) {
    userMenuItems?.splice(1, 0, { key: 'admin', icon: <DashboardOutlined />, label: <Link to="/admin">{t('adminPanel')}</Link> });
  }

  const navMenuItems: MenuProps['items'] = [
    { key: 'home', label: <Link to="/">{t('home')}</Link> },
    { key: 'products', label: <Link to="/products">{t('products')}</Link> },
    { key: 'blog', label: <Link to="/blog">{t('blog')}</Link> },
  ];

  return (
    <>
      <AntHeader className="store-header" style={{ position: 'sticky', top: 0, zIndex: 1000, width: '100%', display: 'flex', alignItems: 'center', padding: '0 24px', background: token.colorBgContainer, borderBottom: '1px solid ' + token.colorBorder }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 24, width: '100%' }}>
          <Link to="/" style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
            <span style={{ fontSize: 20, fontWeight: 600, color: '#1677ff' }}>LampShade</span>
          </Link>

          <div className="desktop-nav" style={{ flex: 1, display: 'flex', justifyContent: 'center' }}>
            <Menu mode="horizontal" items={navMenuItems} style={{ border: 'none', flex: 1, justifyContent: 'center', maxWidth: 600 }} />
          </div>

          <Search className="store-search" placeholder={t('searchProducts')} onSearch={handleSearch} style={{ maxWidth: 400, flex: 1 }} prefix={<SearchOutlined />} />

          <Space size="middle">
            <LanguageToggle />
            <Tooltip title={isDark ? t('lightMode') : t('darkMode')}>
              <Button type="text" icon={isDark ? <SunOutlined /> : <MoonOutlined />} onClick={toggleTheme} />
            </Tooltip>
            <Badge count={0} size="small">
              <Link to="/cart">
                <ShoppingOutlined style={{ fontSize: 22, color: token.colorText }} />
              </Link>
            </Badge>

            <Dropdown menu={{ items: userMenuItems }} placement="bottomRight">
              <Avatar src={user?.profilePhoto ? mediaUrl(user.profilePhoto) : undefined} style={{ backgroundColor: '#1677ff', cursor: 'pointer' }} icon={<UserOutlined />} />
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

