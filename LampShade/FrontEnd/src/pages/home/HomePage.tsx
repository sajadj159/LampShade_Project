import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Row, Col, Typography, Carousel, Card, Button, Spin, Empty } from 'antd';
import { RightOutlined, TruckOutlined, SafetyOutlined, CustomerServiceOutlined } from '@ant-design/icons';
import ProductCard from '../../components/common/ProductCard';
import { productApi, categoryApi, slideApi, mediaUrl } from '../../services/api';
import type { Product, ProductCategory, Slide } from '../../types';

const { Title, Text, Paragraph } = Typography;
const { Meta } = Card;

const HomePage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<ProductCategory[]>([]);
  const [slides, setSlides] = useState<Slide[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [productsData, categoriesData, slidesData] = await Promise.all([
          productApi.getLatest(),
          categoryApi.getWithProducts(),
          slideApi.getAll(),
        ]);
        setProducts(productsData);
        setCategories(categoriesData);
        setSlides(slidesData);
      } catch (error) {
        console.error('Error fetching data:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '60vh' }}>
        <Spin size="large" />
      </div>
    );
  }

  const features = [
    {
      icon: <TruckOutlined style={{ fontSize: 40, color: '#1677ff' }} />,
      title: 'Free Shipping',
      description: 'On orders over $100',
    },
    {
      icon: <SafetyOutlined style={{ fontSize: 40, color: '#52c41a' }} />,
      title: 'Secure Payment',
      description: '100% secure payment',
    },
    {
      icon: <CustomerServiceOutlined style={{ fontSize: 40, color: '#faad14' }} />,
      title: '24/7 Support',
      description: 'Dedicated support',
    },
  ];

  return (
    <div>
      {/* Hero Carousel */}
      {slides.length > 0 && (
        <Carousel autoplay arrows>
          {slides.map((slide) => (
            <div key={slide.id}>
              <div
                style={{
                  height: 400,
                  background: 'linear-gradient(135deg, #1677ff 0%, #0958d9 100%)',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  color: '#fff',
                }}
              >
                <div style={{ textAlign: 'center', padding: '0 24px' }}>
                  <Title level={2} style={{ color: '#fff', margin: 0 }}>
                    {slide.heading}
                  </Title>
                  <Paragraph style={{ color: 'rgba(255,255,255,0.85)', fontSize: 18 }}>
                    {slide.text}
                  </Paragraph>
                  {slide.btnText && (
                    <Button type="primary" size="large" ghost>
                      {slide.btnText}
                    </Button>
                  )}
                </div>
              </div>
            </div>
          ))}
        </Carousel>
      )}

      {/* Features Section */}
      <div style={{ background: '#fff', padding: '48px 24px' }}>
        <Row gutter={[32, 32]} justify="center">
          {features.map((feature, index) => (
            <Col xs={24} sm={8} key={index}>
              <Card
                hoverable
                style={{ textAlign: 'center', height: '100%' }}
              >
                {feature.icon}
                <Title level={4} style={{ marginTop: 16 }}>{feature.title}</Title>
                <Text type="secondary">{feature.description}</Text>
              </Card>
            </Col>
          ))}
        </Row>
      </div>

      {/* Categories Section */}
      {categories.length > 0 && (
        <div style={{ padding: '48px 24px' }}>
          <div style={{ maxWidth: 1200, margin: '0 auto' }}>
            <Title level={2} style={{ textAlign: 'center', marginBottom: 48 }}>
              Shop by Category
            </Title>
            <Row gutter={[24, 24]}>
              {categories.map((category) => (
                <Col xs={24} sm={12} md={8} lg={6} key={category.id}>
                  <Link to={`/category/${category.slug}`}>
                    <Card
                      hoverable
                      cover={
                        <img
                          alt={category.name}
                          src={mediaUrl(category.picture)}
                          style={{ height: 160, objectFit: 'cover' }}
                        />
                      }
                    >
                      <Meta title={category.name} description={`${category.products?.length || 0} products`} />
                    </Card>
                  </Link>
                </Col>
              ))}
            </Row>
          </div>
        </div>
      )}

      {/* Latest Products Section */}
      <div style={{ padding: '48px 24px', background: '#fff' }}>
        <div style={{ maxWidth: 1200, margin: '0 auto' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 32 }}>
            <Title level={2} style={{ margin: 0 }}>Latest Products</Title>
            <Link to="/products">
              <Button type="link">
                View All <RightOutlined />
              </Button>
            </Link>
          </div>
          {products.length === 0 ? (
            <Empty description="No products available" />
          ) : (
            <Row gutter={[24, 24]}>
              {products.slice(0, 8).map((product) => (
                <Col xs={24} sm={12} md={8} lg={6} key={product.id}>
                  <ProductCard product={product} />
                </Col>
              ))}
            </Row>
          )}
        </div>
      </div>
    </div>
  );
};

export default HomePage;
