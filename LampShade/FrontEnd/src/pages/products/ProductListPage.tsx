import React, { useEffect, useState } from 'react';
import { Row, Col, Typography, Spin, Empty, Select, Breadcrumb, Card, Slider } from 'antd';
import { HomeOutlined, ShopOutlined } from '@ant-design/icons';
import ProductCard from '../../components/common/ProductCard';
import { productApi } from '../../services/api';
import type { Product } from '../../types';

const { Title } = Typography;
const { Option } = Select;

const ProductListPage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [sortBy, setSortBy] = useState<string>('default');
  const [priceRange, setPriceRange] = useState<[number, number]>([0, 100000000]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const productsData = await productApi.getLatest();
        setProducts(productsData);
      } catch (error) {
        console.error('Error fetching data:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const filteredProducts = products.filter((product) => {
    const price = product.doublePrice;
    return price >= priceRange[0] && price <= priceRange[1];
  });

  const sortedProducts = [...filteredProducts].sort((a, b) => {
    switch (sortBy) {
      case 'price-low':
        return a.doublePrice - b.doublePrice;
      case 'price-high':
        return b.doublePrice - a.doublePrice;
      case 'name':
        return a.name.localeCompare(b.name);
      default:
        return 0;
    }
  });

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '60vh' }}>
        <Spin size="large" />
      </div>
    );
  }

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Breadcrumb
        items={[
          {
            title: <><HomeOutlined /> Home</>,
            href: '/',
          },
          {
            title: <><ShopOutlined /> Products</>,
          },
        ]}
        style={{ marginBottom: 24 }}
      />

      <Title level={2}>All Products</Title>

      <Row gutter={[24, 24]}>
        {/* Filters Sidebar */}
        <Col xs={24} md={6}>
          <Card title="Filters">
            <div style={{ marginBottom: 24 }}>
              <Title level={5}>Sort By</Title>
              <Select
                value={sortBy}
                onChange={setSortBy}
                style={{ width: '100%' }}
              >
                <Option value="default">Default</Option>
                <Option value="price-low">Price: Low to High</Option>
                <Option value="price-high">Price: High to Low</Option>
                <Option value="name">Name</Option>
              </Select>
            </div>

            <div>
              <Title level={5}>Price Range</Title>
              <Slider
                range
                min={0}
                max={100000000}
                value={priceRange}
                onChange={(value) => setPriceRange(value as [number, number])}
              />
            </div>
          </Card>
        </Col>

        {/* Products Grid */}
        <Col xs={24} md={18}>
          {sortedProducts.length === 0 ? (
            <Empty description="No products found" />
          ) : (
            <Row gutter={[24, 24]}>
              {sortedProducts.map((product) => (
                <Col xs={24} sm={12} lg={8} key={product.id}>
                  <ProductCard product={product} />
                </Col>
              ))}
            </Row>
          )}
        </Col>
      </Row>
    </div>
  );
};

export default ProductListPage;
