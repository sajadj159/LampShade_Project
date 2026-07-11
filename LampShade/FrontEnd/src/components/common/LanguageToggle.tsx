import { Button, Tooltip } from 'antd';
import { TranslationOutlined } from '@ant-design/icons';
import { useLanguage } from '../../contexts/LanguageContext';

const LanguageToggle = () => {
  const { t, toggleLanguage } = useLanguage();
  return <Tooltip title={t('language')}><Button type="text" icon={<TranslationOutlined />} onClick={toggleLanguage}>{t('language')}</Button></Tooltip>;
};

export default LanguageToggle;