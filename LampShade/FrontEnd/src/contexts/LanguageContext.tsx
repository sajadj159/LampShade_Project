import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';

type Language = 'en' | 'fa';

type Dictionary = Record<string, string>;

const dictionaries: Record<Language, Dictionary> = {
  en: {
    home: 'Home', products: 'Products', categories: 'Categories', blog: 'Blog', searchProducts: 'Search products...',
    cart: 'Cart', profile: 'Profile', orderHistory: 'Order History', login: 'Login', register: 'Register', logout: 'Logout',
    adminPanel: 'Admin Panel', dashboard: 'Dashboard', users: 'Users', inventory: 'Inventory', discounts: 'Discounts',
    slides: 'Slides', roles: 'Roles', blogPosts: 'Blog Posts', comments: 'Comments', language: 'FA', lightMode: 'Light mode', darkMode: 'Dark mode',
    quickLinks: 'Quick Links', contact: 'Contact', trustedStore: 'Your trusted online shopping destination for quality products.',
    allRightsReserved: 'All rights reserved.', latestProducts: 'Latest Products', shopByCategory: 'Shop by Category', viewAll: 'View All',
    freeShipping: 'Free Shipping', securePayment: 'Secure Payment', support: '24/7 Support', noProducts: 'No products available',
  },
  fa: {
    home: 'خانه', products: 'محصولات', categories: 'دسته‌بندی‌ها', blog: 'بلاگ', searchProducts: 'جستجوی محصول...',
    cart: 'سبد خرید', profile: 'پروفایل', orderHistory: 'سفارش‌های من', login: 'ورود', register: 'ثبت‌نام', logout: 'خروج',
    adminPanel: 'پنل مدیریت', dashboard: 'داشبورد', users: 'کاربران', inventory: 'موجودی', discounts: 'تخفیف‌ها',
    slides: 'اسلایدها', roles: 'نقش‌ها', blogPosts: 'مطالب وبلاگ', comments: 'دیدگاه‌ها', language: 'EN', lightMode: 'حالت روشن', darkMode: 'حالت تیره',
    quickLinks: 'دسترسی سریع', contact: 'تماس با ما', trustedStore: 'فروشگاه آنلاین شما برای محصولات باکیفیت.',
    allRightsReserved: 'تمامی حقوق محفوظ است.', latestProducts: 'جدیدترین محصولات', shopByCategory: 'خرید بر اساس دسته‌بندی', viewAll: 'مشاهده همه',
    freeShipping: 'ارسال رایگان', securePayment: 'پرداخت امن', support: 'پشتیبانی ۲۴ ساعته', noProducts: 'محصولی موجود نیست',
  },
};

interface LanguageContextValue {
  language: Language;
  isRtl: boolean;
  toggleLanguage: () => void;
  t: (key: string) => string;
}

const LanguageContext = createContext<LanguageContextValue | undefined>(undefined);

export const LanguageProvider = ({ children }: { children: ReactNode }) => {
  const [language, setLanguage] = useState<Language>(() => localStorage.getItem('language') === 'fa' ? 'fa' : 'en');
  const isRtl = language === 'fa';

  useEffect(() => {
    localStorage.setItem('language', language);
    document.documentElement.lang = language;
    document.documentElement.dir = isRtl ? 'rtl' : 'ltr';
  }, [isRtl, language]);

  return <LanguageContext.Provider value={{
    language,
    isRtl,
    toggleLanguage: () => setLanguage(current => current === 'en' ? 'fa' : 'en'),
    t: (key) => dictionaries[language][key] ?? key,
  }}>{children}</LanguageContext.Provider>;
};

export const useLanguage = () => {
  const context = useContext(LanguageContext);
  if (!context) throw new Error('useLanguage must be used within LanguageProvider');
  return context;
};