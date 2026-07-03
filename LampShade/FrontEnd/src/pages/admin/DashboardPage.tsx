import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Statistic, Typography, Spin } from 'antd';
import { UserOutlined, ShoppingOutlined, PercentageOutlined, ReadOutlined } from '@ant-design/icons';
import { accountApi, productApi, discountApi, articleApi } from '../../services/api';

const { Title } = Typography;

const DashboardPage: React.FC = () => {
  const [loading, setLoading] = useState(true);
  const [stats, setStats] = useState({ users: 0, products: 0, discounts: 0, articles: 0 });

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const [users, products, discounts, articles] = await Promise.allSettled([
          accountApi.getAll(),
          productApi.getAll(),
          discountApi.search({}),
          articleApi.search({}),
        ]);
        setStats({
          users: users.status === 'fulfilled' ? users.value.length : 0,
          products: products.status === 'fulfilled' ? products.value.length : 0,
          discounts: discounts.status === 'fulfilled' ? discounts.value.length : 0,
          articles: articles.status === 'fulfilled' ? articles.value.length : 0,
        });
      } finally {
        setLoading(false);
      }
    };
    fetchStats();
  }, []);

  if (loading) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  return (
    <div>
      <Title level={3}>Dashboard</Title>
      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Users" value={stats.users} prefix={<UserOutlined />} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Products" value={stats.products} prefix={<ShoppingOutlined />} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Discounts" value={stats.discounts} prefix={<PercentageOutlined />} />
          </Card>
        </Col>
        <Col xs={24} sm={12} lg={6}>
          <Card>
            <Statistic title="Blog Posts" value={stats.articles} prefix={<ReadOutlined />} />
          </Card>
        </Col>
      </Row>
    </div>
  );
};

export default DashboardPage;
