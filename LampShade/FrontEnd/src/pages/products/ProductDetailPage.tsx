import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  Row,
  Col,
  Typography,
  Image,
  Button,
  InputNumber,
  Tag,
  Tabs,
  Form,
  Input,
  Rate,
  message,
  Spin,
  Empty,
  Divider,
  Space,
} from 'antd';
import { ShoppingCartOutlined, HomeOutlined, RightOutlined } from '@ant-design/icons';
import { productApi, commentApi, mediaUrl } from '../../services/api';
import type { Product } from '../../types';
import { useAuth } from '../../contexts/AuthContext';

const { Title, Text, Paragraph } = Typography;
const { TabPane } = Tabs;
const { TextArea } = Input;

const ProductDetailPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const { isAuthenticated, user } = useAuth();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);
  const [quantity, setQuantity] = useState(1);
  const [commentForm] = Form.useForm();

  useEffect(() => {
    const fetchProduct = async () => {
      if (!slug) return;
      try {
        const data = await productApi.getBySlug(slug);
        setProduct(data);
      } catch (error) {
        console.error('Error fetching product:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchProduct();
  }, [slug]);

  useEffect(() => {
    if (!isAuthenticated || !user) return;

    const email = user.username?.includes('@') ? user.username : undefined;
    commentForm.setFieldsValue({
      name: user.fullname || user.username,
      ...(email ? { email } : {}),
    });
  }, [commentForm, isAuthenticated, user]);

  const handleAddToCart = () => {
    if (!product) return;
    
    const cartItems = JSON.parse(localStorage.getItem('cartItems') || '[]');
    const existingItem = cartItems.find((item: any) => item.id === product.id);
    
    if (existingItem) {
      existingItem.count += quantity;
    } else {
      cartItems.push({
        id: product.id,
        name: product.name,
        unitPrice: product.doublePrice,
        pictureUrl: product.pictureUrl,
        count: quantity,
      });
    }
    
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    message.success('Added to cart!');
  };

  const handleCommentSubmit = async (values: any) => {
    if (!product) return;
    
    try {
      await commentApi.add({
        name: values.name,
        email: values.email,
        description: values.description,
        ownerRecordId: product.id,
        type: 1,
        rating: values.rating,
      });
      message.success('Comment submitted successfully!');
      commentForm.resetFields();
    } catch (error) {
      message.error('Failed to submit comment');
    }
  };

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '60vh' }}>
        <Spin size="large" />
      </div>
    );
  }

  if (!product) {
    return <Empty description="Product not found" />;
  }

  return (
    <div style={{ padding: '24px', maxWidth: 1200, margin: '0 auto' }}>
      {/* Breadcrumb */}
      <Space style={{ marginBottom: 24 }}>
        <Link to="/"><HomeOutlined /> Home</Link>
        <RightOutlined />
        <Link to={`/category/${product.categorySlug}`}>{product.category}</Link>
        <RightOutlined />
        <Text>{product.name}</Text>
      </Space>

      {/* Product Details */}
      <Row gutter={[32, 32]}>
        {/* Product Images */}
        <Col xs={24} md={12}>
          <Image
            src={mediaUrl(product.pictureUrl)}
            alt={product.pictureAlt}
            style={{ width: '100%', maxHeight: 400, objectFit: 'contain' }}
            fallback="https://via.placeholder.com/400"
          />
          {product.pictures && product.pictures.length > 0 && (
            <Row gutter={8} style={{ marginTop: 16 }}>
              {product.pictures.map((pic) => (
                <Col span={6} key={pic.id}>
                  <Image
                    src={mediaUrl(pic.pictureUrl)}
                    alt={pic.pictureAlt}
                    style={{ width: '100%', height: 80, objectFit: 'cover' }}
                    fallback="https://via.placeholder.com/80"
                  />
                </Col>
              ))}
            </Row>
          )}
        </Col>

        {/* Product Info */}
        <Col xs={24} md={12}>
          <Tag color="blue">{product.category}</Tag>
          <Title level={2} style={{ marginTop: 16 }}>{product.name}</Title>
          
          <Space style={{ marginBottom: 16 }}>
            <Rate disabled defaultValue={4} />
            <Text type="secondary">({product.comments?.length || 0} reviews)</Text>
          </Space>

          <div style={{ marginBottom: 16 }}>
            {product.hasDiscount ? (
              <Space size="large">
                <Text strong style={{ fontSize: 28, color: '#ff4d4f' }}>
                  {product.priceWithDiscount}
                </Text>
                <Text delete type="secondary" style={{ fontSize: 18 }}>
                  {product.price}
                </Text>
                <Tag color="red">-{product.discountRate}%</Tag>
              </Space>
            ) : (
              <Text strong style={{ fontSize: 28 }}>{product.price}</Text>
            )}
          </div>

          <Divider />

          <Space direction="vertical" size="small" style={{ width: '100%' }}>
            <Text>
              <Text strong>Code: </Text>{product.code}
            </Text>
            <Text>
              <Text strong>Availability: </Text>
              <Tag color={product.inStock ? 'green' : 'red'}>
                {product.inStock ? 'In Stock' : 'Out of Stock'}
              </Tag>
            </Text>
          </Space>

          <Paragraph style={{ marginTop: 16 }}>{product.shortDescription}</Paragraph>

          {product.inStock && (
            <div style={{ marginTop: 24 }}>
              <Space>
                <Text strong>Quantity:</Text>
                <InputNumber
                  min={1}
                  max={100}
                  value={quantity}
                  onChange={(value) => setQuantity(value || 1)}
                />
                <Button
                  type="primary"
                  size="large"
                  icon={<ShoppingCartOutlined />}
                  onClick={handleAddToCart}
                >
                  Add to Cart
                </Button>
              </Space>
            </div>
          )}

          {!product.inStock && (
            <Button
              type="primary"
              size="large"
              danger
              disabled
              style={{ marginTop: 24 }}
            >
              Out of Stock
            </Button>
          )}
        </Col>
      </Row>

      {/* Tabs Section */}
      <Tabs defaultActiveKey="1" style={{ marginTop: 48 }}>
        <TabPane tab="Description" key="1">
          <article className="product-description" dangerouslySetInnerHTML={{ __html: product.description }} />
        </TabPane>
        <TabPane tab={`Reviews (${product.comments?.length || 0})`} key="2">
          {product.comments && product.comments.length > 0 ? (
            <div>
              {product.comments.map((comment) => (
                <div key={comment.id} style={{ marginBottom: 24, padding: 16, background: '#fafafa', borderRadius: 8 }}>
                  <Space><Text strong>{comment.name}</Text><Rate disabled value={comment.rating} /></Space>
                  <Paragraph style={{ marginTop: 8 }}>{comment.description}</Paragraph>
                </div>
              ))}
            </div>
          ) : (
            <Empty description="No reviews yet" />
          )}

          <Divider />

          <Title level={4}>Write a Review</Title>
          <Form form={commentForm} layout="vertical" onFinish={handleCommentSubmit} style={{ maxWidth: 600 }}>
            <Form.Item name="name" label="Name" rules={[{ required: true }]}>
              <Input placeholder="Your name" />
            </Form.Item>
            <Form.Item name="email" label="Email" rules={[{ required: true, type: 'email' }]}>
              <Input placeholder="Your email" />
            </Form.Item>
            <Form.Item name="rating" label="Rating" initialValue={0} rules={[{ required: true }]}>
              <Rate allowClear />
            </Form.Item>
            <Form.Item name="description" label="Review" rules={[{ required: true }]}>
              <TextArea rows={4} placeholder="Write your review..." />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit">Submit Review</Button>
            </Form.Item>
          </Form>
        </TabPane>
      </Tabs>
    </div>
  );
};

export default ProductDetailPage;
