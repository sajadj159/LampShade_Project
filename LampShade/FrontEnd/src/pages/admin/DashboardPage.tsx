import React, { useEffect, useMemo, useState } from 'react';
import { Row, Col, Card, Statistic, Typography, Spin, Empty } from 'antd';
import { UserOutlined, ShoppingOutlined, PercentageOutlined, ReadOutlined } from '@ant-design/icons';
import Chart from 'react-apexcharts';
import type { ApexOptions } from 'apexcharts';
import { accountApi, productApi, discountApi, articleApi } from '../../services/api';

const { Title, Text } = Typography;

type DashboardStats = {
  users: number;
  products: number;
  discounts: number;
  articles: number;
};

const DashboardPage: React.FC = () => {
  const [loading, setLoading] = useState(true);
  const [stats, setStats] = useState<DashboardStats>({ users: 0, products: 0, discounts: 0, articles: 0 });

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

    void fetchStats();
  }, []);

  const values = useMemo(() => [stats.users, stats.products, stats.discounts, stats.articles], [stats]);

  const comparisonOptions = useMemo<ApexOptions>(() => ({
    chart: { type: 'bar', toolbar: { show: false }, fontFamily: 'Inter, Vazirmatn, sans-serif' },
    colors: ['#1677ff'],
    plotOptions: { bar: { borderRadius: 6, columnWidth: '48%', distributed: true } },
    dataLabels: { enabled: false },
    legend: { show: false },
    xaxis: { categories: ['Users', 'Products', 'Discounts', 'Blog posts'] },
    yaxis: { min: 0, forceNiceScale: true, labels: { formatter: (value) => Math.round(value).toString() } },
    grid: { borderColor: '#f0f0f0' },
    tooltip: { y: { formatter: (value) => `${Math.round(value)} records` } },
  }), []);

  const distributionOptions = useMemo<ApexOptions>(() => ({
    chart: { type: 'donut', fontFamily: 'Inter, Vazirmatn, sans-serif' },
    labels: ['Users', 'Products', 'Discounts', 'Blog posts'],
    colors: ['#1677ff', '#52c41a', '#faad14', '#722ed1'],
    legend: { position: 'bottom' },
    dataLabels: { enabled: false },
    stroke: { colors: ['#fff'] },
    tooltip: { y: { formatter: (value) => `${Math.round(value)} records` } },
    plotOptions: { pie: { donut: { size: '68%', labels: { show: true, total: { show: true, label: 'Records', formatter: () => values.reduce((sum, value) => sum + value, 0).toString() } } } } },
  }), [values]);

  if (loading) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

  const hasData = values.some((value) => value > 0);

  return (
    <div>
      <Title level={3}>Dashboard</Title>
      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12} lg={6}><Card><Statistic title="Users" value={stats.users} prefix={<UserOutlined />} /></Card></Col>
        <Col xs={24} sm={12} lg={6}><Card><Statistic title="Products" value={stats.products} prefix={<ShoppingOutlined />} /></Card></Col>
        <Col xs={24} sm={12} lg={6}><Card><Statistic title="Discounts" value={stats.discounts} prefix={<PercentageOutlined />} /></Card></Col>
        <Col xs={24} sm={12} lg={6}><Card><Statistic title="Blog Posts" value={stats.articles} prefix={<ReadOutlined />} /></Card></Col>
      </Row>

      <Row gutter={[16, 16]} style={{ marginTop: 16 }}>
        <Col xs={24} lg={14}>
          <Card title="Admin records overview">
            {hasData ? <Chart options={comparisonOptions} series={[{ name: 'Records', data: values }]} type="bar" height={320} /> : <Empty description="No dashboard data yet" />}
          </Card>
        </Col>
        <Col xs={24} lg={10}>
          <Card title="Record distribution">
            {hasData ? <Chart options={distributionOptions} series={values} type="donut" height={320} /> : <Empty description="No dashboard data yet" />}
          </Card>
        </Col>
      </Row>
      <Text type="secondary" style={{ display: 'block', marginTop: 12 }}>Charts refresh when the dashboard is opened.</Text>
    </div>
  );
};

export default DashboardPage;