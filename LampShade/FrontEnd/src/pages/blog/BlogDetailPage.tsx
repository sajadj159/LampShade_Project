import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Typography, Tag, Spin, Empty, Breadcrumb, Space, Divider } from 'antd';
import { CalendarOutlined, HomeOutlined, ReadOutlined } from '@ant-design/icons';
import { articleApi } from '../../services/api';
import type { Article } from '../../types';

const { Title, Text } = Typography;

const BlogDetailPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const [article, setArticle] = useState<Article | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchArticle = async () => {
      if (!slug) return;
      try {
        const data = await articleApi.getBySlug(slug);
        setArticle(data);
      } catch {
        console.error('Failed to load article');
      } finally {
        setLoading(false);
      }
    };
    fetchArticle();
  }, [slug]);

  if (loading) {
    return <div style={{ display: 'flex', justifyContent: 'center', padding: '100px 0' }}><Spin size="large" /></div>;
  }

  if (!article) {
    return <Empty description="Article not found" style={{ padding: '100px 0' }} />;
  }

  return (
    <div style={{ padding: '24px', maxWidth: 800, margin: '0 auto' }}>
      <Breadcrumb
        items={[
          { title: <Link to="/"><HomeOutlined /> Home</Link> },
          { title: <Link to="/blog"><ReadOutlined /> Blog</Link> },
          { title: article.title },
        ]}
        style={{ marginBottom: 24 }}
      />

      <Title level={1}>{article.title}</Title>

      <Space style={{ marginBottom: 16 }}>
        <Tag color="blue">{article.category}</Tag>
        <Text type="secondary"><CalendarOutlined /> {article.publishDate}</Text>
      </Space>

      <img
        src={`http://localhost:5002/Pictures/${article.picture}`}
        alt={article.title}
        style={{ width: '100%', maxHeight: 400, objectFit: 'cover', borderRadius: 8, marginBottom: 24 }}
      />

      <Divider />

      <div dangerouslySetInnerHTML={{ __html: article.description }} style={{ lineHeight: 1.8, fontSize: 16 }} />
    </div>
  );
};

export default BlogDetailPage;
