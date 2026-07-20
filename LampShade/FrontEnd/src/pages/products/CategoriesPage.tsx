import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Row, Col, Typography, Card, Spin, Empty, Breadcrumb } from 'antd';
import { HomeOutlined, AppstoreOutlined } from '@ant-design/icons';
import { categoryApi, mediaUrl } from '../../services/api';
import type { ProductCategory } from '../../types';

const { Title, Text } = Typography;
const { Meta } = Card;

const CategoriesPage: React.FC = () => {
  const [categories, setCategories] = useState<ProductCategory[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await categoryApi.getWithProducts();
        setCategories(data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchCategories();
  }, []);

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
            title: <><AppstoreOutlined /> Categories</>,
          },
        ]}
        style={{ marginBottom: 24 }}
      />

      <Title level={2}>Shop by Category</Title>

      {categories.length === 0 ? (
        <Empty description="No categories available" />
      ) : (
        <Row gutter={[24, 24]}>
          {categories.map((category) => (
            <Col xs={24} sm={12} md={8} lg={6} key={category.id}>
              <Link to={`/category/${category.slug}`}>
                <Card
                  hoverable
                  cover={
                    <img
                      alt={category.name}
                      src={mediaUrl(category.pictureUrl || category.picture || '')}
                      className="catalog-card__image"
                      onError={(e) => {
                        (e.target as HTMLImageElement).src = 'https://via.placeholder.com/300x200';
                      }}
                    />
                  }
                >
                  <Meta
                    title={category.name}
                    description={
                      <div>
                        <Text type="secondary">
                          {category.products?.length || 0} products
                        </Text>
                        {category.description && (
                          <Text
                            ellipsis
                            style={{ display: 'block', marginTop: 8 }}
                          >
                            {category.description}
                          </Text>
                        )}
                      </div>
                    }
                  />
                </Card>
              </Link>
            </Col>
          ))}
        </Row>
      )}
    </div>
  );
};

export default CategoriesPage;

