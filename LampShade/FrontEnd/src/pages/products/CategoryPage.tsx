import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Row, Col, Typography, Spin, Empty, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import ProductCard from '../../components/common/ProductCard';
import { categoryApi } from '../../services/api';
import type { ProductCategory } from '../../types';

const { Title, Paragraph } = Typography;

const CategoryPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const [category, setCategory] = useState<ProductCategory | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCategory = async () => {
      if (!slug) return;
      try {
        const data = await categoryApi.getBySlug(slug);
        setCategory(data);
      } catch (error) {
        console.error('Error fetching category:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchCategory();
  }, [slug]);

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '60vh' }}>
        <Spin size="large" />
      </div>
    );
  }

  if (!category) {
    return <Empty description="Category not found" />;
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
            title: category.name,
          },
        ]}
        style={{ marginBottom: 24 }}
      />

      <Title level={2}>{category.name}</Title>
      {category.description && (
        <Paragraph type="secondary" style={{ marginBottom: 32 }}>
          {category.description}
        </Paragraph>
      )}

      {category.products && category.products.length > 0 ? (
        <Row gutter={[24, 24]}>
          {category.products.map((product) => (
            <Col xs={24} sm={12} md={8} lg={6} key={product.id}>
              <ProductCard product={product} />
            </Col>
          ))}
        </Row>
      ) : (
        <Empty description="No products in this category" />
      )}
    </div>
  );
};

export default CategoryPage;
