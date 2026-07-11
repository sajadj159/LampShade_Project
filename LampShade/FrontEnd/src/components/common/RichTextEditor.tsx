import { useEffect, useRef, useState } from 'react';
import { Button, Divider, Select, Tooltip, message, theme } from 'antd';
import {
  AlignCenterOutlined,
  AlignLeftOutlined,
  AlignRightOutlined,
  BoldOutlined,
  ClearOutlined,
  ItalicOutlined,
  LinkOutlined,
  OrderedListOutlined,
  PictureOutlined,
  RedoOutlined,
  UnderlineOutlined,
  UndoOutlined,
  UnorderedListOutlined,
} from '@ant-design/icons';
import { EditorContent, useEditor } from '@tiptap/react';
import StarterKit from '@tiptap/starter-kit';
import Image from '@tiptap/extension-image';
import Link from '@tiptap/extension-link';
import TextAlign from '@tiptap/extension-text-align';
import Underline from '@tiptap/extension-underline';
import { editorImageApi, mediaUrl } from '../../services/api';

interface RichTextEditorProps {
  value?: string;
  onChange?: (value: string) => void;
}

const RichTextEditor = ({ value = '', onChange }: RichTextEditorProps) => {
  const imageInputRef = useRef<HTMLInputElement>(null);
  const [uploading, setUploading] = useState(false);
  const { token } = theme.useToken();

  const editor = useEditor({
    immediatelyRender: false,
    extensions: [
      StarterKit.configure({ heading: { levels: [2, 3] } }),
      Underline,
      Link.configure({ openOnClick: false, autolink: true }),
      Image.configure({ allowBase64: false }),
      TextAlign.configure({ types: ['heading', 'paragraph'] }),
    ],
    content: value,
    editorProps: {
      attributes: {
        class: 'rich-text-editor__canvas tiptap',
      },
    },
    onUpdate: ({ editor: currentEditor }) => onChange?.(currentEditor.getHTML()),
  });

  useEffect(() => {
    if (editor && value !== editor.getHTML()) {
      editor.commands.setContent(value, { emitUpdate: false });
    }
  }, [editor, value]);

  const uploadImage = async (file?: File) => {
    if (!editor || !file) return;
    if (!file.type.startsWith('image/') || file.size > 5 * 1024 * 1024) {
      message.error('Choose an image up to 5 MB.');
      return;
    }

    setUploading(true);
    try {
      const key = await editorImageApi.upload(file);
      editor.chain().focus().setImage({ src: mediaUrl(key), alt: file.name }).run();
    } catch {
      message.error('Image upload failed.');
    } finally {
      setUploading(false);
      if (imageInputRef.current) imageInputRef.current.value = '';
    }
  };

  const addLink = () => {
    if (!editor) return;
    const url = window.prompt('Link URL', editor.getAttributes('link').href || '');
    if (url === null) return;
    if (url === '') {
      editor.chain().focus().extendMarkRange('link').unsetLink().run();
      return;
    }
    editor.chain().focus().extendMarkRange('link').setLink({ href: url }).run();
  };

  if (!editor) return null;

  const toolButton = (title: string, icon: React.ReactNode, action: () => void, active = false) => (
    <Tooltip title={title}>
      <Button type={active ? 'primary' : 'text'} size="small" icon={icon} onMouseDown={event => event.preventDefault()} onClick={action} />
    </Tooltip>
  );

  return <div className="rich-text-editor" style={{ borderColor: token.colorBorder, background: token.colorBgContainer }}>
    <div className="rich-text-editor__toolbar" style={{ background: token.colorFillAlter, borderColor: token.colorBorder }}>
      <div className="rich-text-editor__group">
        <Select
          aria-label="Text style"
          size="small"
          style={{ width: 132 }}
          value={editor.isActive('heading', { level: 2 }) ? 'h2' : editor.isActive('heading', { level: 3 }) ? 'h3' : 'p'}
          onChange={(format) => {
            const chain = editor.chain().focus();
            if (format === 'h2') chain.toggleHeading({ level: 2 }).run();
            else if (format === 'h3') chain.toggleHeading({ level: 3 }).run();
            else chain.setParagraph().run();
          }}
          options={[
            { value: 'p', label: 'Paragraph' },
            { value: 'h2', label: 'Heading 1' },
            { value: 'h3', label: 'Heading 2' },
          ]}
        />
      </div>
      <Divider type="vertical" />
      <div className="rich-text-editor__group">
        {toolButton('Bold', <BoldOutlined />, () => editor.chain().focus().toggleBold().run(), editor.isActive('bold'))}
        {toolButton('Italic', <ItalicOutlined />, () => editor.chain().focus().toggleItalic().run(), editor.isActive('italic'))}
        {toolButton('Underline', <UnderlineOutlined />, () => editor.chain().focus().toggleUnderline().run(), editor.isActive('underline'))}
      </div>
      <Divider type="vertical" />
      <div className="rich-text-editor__group">
        {toolButton('Bulleted list', <UnorderedListOutlined />, () => editor.chain().focus().toggleBulletList().run(), editor.isActive('bulletList'))}
        {toolButton('Numbered list', <OrderedListOutlined />, () => editor.chain().focus().toggleOrderedList().run(), editor.isActive('orderedList'))}
        {toolButton('Align left', <AlignLeftOutlined />, () => editor.chain().focus().setTextAlign('left').run(), editor.isActive({ textAlign: 'left' }))}
        {toolButton('Align center', <AlignCenterOutlined />, () => editor.chain().focus().setTextAlign('center').run(), editor.isActive({ textAlign: 'center' }))}
        {toolButton('Align right', <AlignRightOutlined />, () => editor.chain().focus().setTextAlign('right').run(), editor.isActive({ textAlign: 'right' }))}
      </div>
      <Divider type="vertical" />
      <div className="rich-text-editor__group">
        {toolButton('Add link', <LinkOutlined />, addLink, editor.isActive('link'))}
        <Tooltip title="Add image"><Button type="text" size="small" loading={uploading} icon={<PictureOutlined />} onClick={() => imageInputRef.current?.click()}>Add</Button></Tooltip>
      </div>
      <div className="rich-text-editor__spacer" />
      <div className="rich-text-editor__group">
        {toolButton('Clear formatting', <ClearOutlined />, () => editor.chain().focus().clearNodes().unsetAllMarks().run())}
        {toolButton('Undo', <UndoOutlined />, () => editor.chain().focus().undo().run())}
        {toolButton('Redo', <RedoOutlined />, () => editor.chain().focus().redo().run())}
      </div>
    </div>
    <input ref={imageInputRef} className="rich-text-editor__file-input" type="file" accept="image/*" onChange={(event) => uploadImage(event.target.files?.[0])} />
    <EditorContent editor={editor} />
  </div>;
};

export default RichTextEditor;