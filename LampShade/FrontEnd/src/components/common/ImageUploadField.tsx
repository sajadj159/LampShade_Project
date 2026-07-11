import { Button, Form, Image, Upload, message } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import { mediaUrl } from '../../services/api';

const maximumFileSizeInBytes = 5 * 1024 * 1024;

const normalizeFileList = (event: Parameters<NonNullable<UploadProps['onChange']>>[0]) =>
  Array.isArray(event) ? event : event?.fileList ?? [];

const validateImage: UploadProps['beforeUpload'] = (file) => {
  if (!file.type.startsWith('image/')) {
    message.error('Please choose an image file.');
    return Upload.LIST_IGNORE;
  }

  if (file.size > maximumFileSizeInBytes) {
    message.error('Image size must be 5 MB or smaller.');
    return Upload.LIST_IGNORE;
  }

  return false;
};

interface ImageUploadFieldProps {
  name: string;
  label: string;
  currentImage?: string;
  required?: boolean;
}

const ImageUploadField = ({ name, label, currentImage, required = false }: ImageUploadFieldProps) => (
  <>
    {currentImage && (
      <Image
        alt={`Current ${label.toLowerCase()}`}
        height={96}
        preview
        src={mediaUrl(currentImage)}
        style={{ display: 'block', marginBottom: 12, maxWidth: '100%', objectFit: 'cover' }}
        width={96}
      />
    )}
    <Form.Item
      getValueFromEvent={normalizeFileList}
      label={label}
      name={name}
      rules={required ? [{ required: true, message: `Choose a ${label.toLowerCase()}` }] : undefined}
      valuePropName="fileList"
    >
      <Upload accept="image/*" beforeUpload={validateImage} listType="picture" maxCount={1}>
        <Button icon={<UploadOutlined />}>Choose Image</Button>
      </Upload>
    </Form.Item>
  </>
);

export default ImageUploadField;
