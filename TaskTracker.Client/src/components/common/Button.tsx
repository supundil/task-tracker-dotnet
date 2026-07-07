interface ButtonProps
    extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
}

export default function Button({
  children,
  ...props
}: ButtonProps) {
  return (
    <button
      {...props}
      className="w-full rounded-lg bg-blue-600 px-4 py-3 text-white font-semibold transition hover:bg-blue-700 disabled:opacity-50"
    >
      {children}
    </button>
  );
}