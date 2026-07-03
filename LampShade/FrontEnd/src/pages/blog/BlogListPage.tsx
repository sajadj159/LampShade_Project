import React, { useEffect, useState } from 'react';
import { Row, Col, Card, Typography, Spin, Empty, Tag } from 'antd';
import { CalendarOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import { articleApi } from '../../services/api';
import type { Article } from '../../types';

const { Title, Paragraph, Text } = Typography;
const { Meta } = Card;

const BlogListPage: React.FC = () => {
  const [articles, setArticles] = useState<Article[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchArticles = async () => {
      try {
        const data = await articleApi.getLatest();
        setArticles(data);
      } catch {
        console.error('Failed to load articles');
      } finally {
        setLoading(false);
      }
    };
    fetchArticles();
  }, []);

  if (loading) {
    return <div style={{ display: 'flex', justifyContent: 'center', padding: '100px 0' }}><Spin size="large" /></div>;
  }

  if (articles.length === 0) {
    return <Empty description="No articles found" style={{ padding: '100px 0' }} />;
  }

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      <Title level={2} style={{ textAlign: 'center', marginBottom: 48 }}>Blog</Title>
      <Row gutter={[24, 24]}>
        {articles.map((article) => (
          <Col xs={24} sm={12} md={8} key={article.id}>
            <Link to={`/blog/${article.slug}`}>
              <Card
                hoverable
                cover={
                  <img
                    alt={article.title}
                    src={`http://localhost:5002/Pictures/${article.picture}`}
                    style={{ height: 200, objectFit: 'cover' }}
                  />
                }
              >
                <Meta
                  title={article.title}
                  description={
                    <div>
                      <Tag color="blue">{article.category}</Tag>
                      <div style={{ marginTop: 8 }}>
                        <Text type="secondary">
                          <CalendarOutlined /> {article.publishDate}
                        </Text>
                      </div>
                      <Paragraph ellipsis={{ rows: 2 }} style={{ marginTop: 8 }}>
                        {article.shortDescription}
                      </Paragraph>
                    </div>
                  }
                />
              </Card>
            </Link>
          </Col>
        ))}
      </Row>
    </div>
  );
};

export default BlogListPage;
