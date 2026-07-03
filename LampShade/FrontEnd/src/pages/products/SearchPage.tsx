import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { Row, Col, Typography, Spin, Empty, Input, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import ProductCard from '../../components/common/ProductCard';
import { productApi } from '../../services/api';
import type { Product } from '../../types';

const { Title } = Typography;
const { Search } = Input;

const SearchPage: React.FC = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const query = searchParams.get('q') || '';
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (query) {
      searchProducts(query);
    }
  }, [query]);

  const searchProducts = async (value: string) => {
    setLoading(true);
    try {
      const results = await productApi.search(value);
      setProducts(results);
    } catch (error) {
      console.error('Search error:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = (value: string) => {
    setSearchParams({ q: value });
  };

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Breadcrumb
        items={[
          {
            title: <><HomeOutlined /> Home</>,
            href: '/',
          },
          {
            title: <>Search Results</>,
          },
        ]}
        style={{ marginBottom: 24 }}
      />

      <Title level={2}>Search Products</Title>

      <Search
        placeholder="Search for products..."
        onSearch={handleSearch}
        defaultValue={query}
        enterButton
        size="large"
        style={{ marginBottom: 32 }}
      />

      {loading ? (
        <div style={{ display: 'flex', justifyContent: 'center', padding: '48px 0' }}>
          <Spin size="large" />
        </div>
      ) : products.length === 0 ? (
        <Empty description={query ? `No results found for "${query}"` : 'Enter a search term'} />
      ) : (
        <>
          <Title level={4} type="secondary" style={{ marginBottom: 24 }}>
            {products.length} results found for "{query}"
          </Title>
          <Row gutter={[24, 24]}>
            {products.map((product) => (
              <Col xs={24} sm={12} md={8} lg={6} key={product.id}>
                <ProductCard product={product} />
              </Col>
            ))}
          </Row>
        </>
      )}
    </div>
  );
};

export default SearchPage;
