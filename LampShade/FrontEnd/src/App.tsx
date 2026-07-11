import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ConfigProvider, theme as antdTheme } from 'antd';
import faIR from 'antd/locale/fa_IR';
import enUS from 'antd/locale/en_US';
import { AuthProvider } from './contexts/AuthContext';
import { ThemeModeProvider, useThemeMode } from './contexts/ThemeModeContext';
import { LanguageProvider, useLanguage } from './contexts/LanguageContext';
import MainLayout from './components/layout/MainLayout';
import AdminLayout from './components/layout/AdminLayout';
import ProtectedRoute from './components/common/ProtectedRoute';

// Public Pages
import HomePage from './pages/home/HomePage';
import ProductListPage from './pages/products/ProductListPage';
import ProductDetailPage from './pages/products/ProductDetailPage';
import CategoryPage from './pages/products/CategoryPage';
import CategoriesPage from './pages/products/CategoriesPage';
import SearchPage from './pages/products/SearchPage';
import CartPage from './pages/cart/CartPage';
import CheckoutPage from './pages/checkout/CheckoutPage';
import LoginPage from './pages/auth/LoginPage';
import RegisterPage from './pages/auth/RegisterPage';
import ProfilePage from './pages/account/ProfilePage';
import OrdersPage from './pages/account/OrdersPage';

// Blog Pages
import BlogListPage from './pages/blog/BlogListPage';
import BlogDetailPage from './pages/blog/BlogDetailPage';

// Admin Pages
import DashboardPage from './pages/admin/DashboardPage';
import UsersPage from './pages/admin/UsersPage';
import ProductsPage from './pages/admin/ProductsPage';
import AdminCategoriesPage from './pages/admin/CategoriesPage';
import DiscountsPage from './pages/admin/DiscountsPage';
import SlidesPage from './pages/admin/SlidesPage';
import RolesPage from './pages/admin/RolesPage';
import BlogAdminPage from './pages/admin/BlogPage';
import CommentsPage from './pages/admin/CommentsPage';
import InventoryPage from './pages/admin/InventoryPage';

const ThemedApp: React.FC = () => {
  const { isDark } = useThemeMode();
  const { isRtl } = useLanguage();

  return (
    <ConfigProvider
      direction={isRtl ? 'rtl' : 'ltr'}
      locale={isRtl ? faIR : enUS}
      theme={{
        algorithm: isDark ? antdTheme.darkAlgorithm : antdTheme.defaultAlgorithm,
        token: {
          colorPrimary: '#1677ff',
          borderRadius: 8,
          fontFamily: isRtl ? '"Vazirmatn Variable", Tahoma, sans-serif' : '"Inter Variable", Inter, sans-serif',
        },
      }}
    >
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            {/* Public Routes */}
            <Route path="/" element={<MainLayout />}>
              <Route index element={<HomePage />} />
              <Route path="products" element={<ProductListPage />} />
              <Route path="product/:slug" element={<ProductDetailPage />} />
              <Route path="category/:slug" element={<CategoryPage />} />
              <Route path="categories" element={<CategoriesPage />} />
              <Route path="search" element={<SearchPage />} />
              <Route path="cart" element={<CartPage />} />
              <Route path="checkout" element={
                <ProtectedRoute><CheckoutPage /></ProtectedRoute>
              } />
              <Route path="login" element={<LoginPage />} />
              <Route path="register" element={<RegisterPage />} />
              <Route path="account/profile" element={
                <ProtectedRoute><ProfilePage /></ProtectedRoute>
              } />
              <Route path="account/orders" element={
                <ProtectedRoute><OrdersPage /></ProtectedRoute>
              } />

              {/* Blog Routes */}
              <Route path="blog" element={<BlogListPage />} />
              <Route path="blog/:slug" element={<BlogDetailPage />} />
            </Route>

            {/* Admin Routes */}
            <Route path="/admin" element={
              <ProtectedRoute adminOnly><AdminLayout /></ProtectedRoute>
            }>
              <Route index element={<DashboardPage />} />
              <Route path="users" element={<UsersPage />} />
              <Route path="products" element={<ProductsPage />} />
              <Route path="categories" element={<AdminCategoriesPage />} />
              <Route path="discounts" element={<DiscountsPage />} />
              <Route path="slides" element={<SlidesPage />} />
              <Route path="roles" element={<RolesPage />} />
              <Route path="blog" element={<BlogAdminPage />} />
              <Route path="comments" element={<CommentsPage />} />
              <Route path="inventory" element={<InventoryPage />} />
            </Route>
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </ConfigProvider>
  );
};

const App: React.FC = () => <LanguageProvider><ThemeModeProvider><ThemedApp /></ThemeModeProvider></LanguageProvider>;

export default App;
