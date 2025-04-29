'use client';

import { useEffect, useState, useCallback } from 'react';

interface Product {
  id: number;
  name: string;
  price: number;
}

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [newProductName, setNewProductName] = useState('');
  const [newProductPrice, setNewProductPrice] = useState(0);

  const apiUrl = `${process.env.NEXT_PUBLIC_API_BASE_URL}/api/products`;

  console.log(apiUrl);

  const fetchProducts = useCallback(async () => {
    try {
      const res = await fetch(apiUrl);
      if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
      const data = await res.json();
      setProducts(data);
    } catch (error) {
      console.error('Failed to fetch products:', error);
    }
  }, [apiUrl]); // Include apiUrl as a dependency

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  const createProduct = async () => {
    try {
      const res = await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: newProductName, price: newProductPrice })
      });

      if (!res.ok) throw new Error(`Failed to create: ${res.status}`);
      setNewProductName('');
      setNewProductPrice(0);
      fetchProducts();
    } catch (error) {
      console.error('Error creating product:', error);
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h1>Products</h1>

      <div style={{ marginBottom: '2rem' }}>
        <input
          type="text"
          placeholder="Product name"
          value={newProductName}
          onChange={(e) => setNewProductName(e.target.value)}
          style={{ marginRight: '1rem' }}
        />
        <input
          type="number"
          placeholder="Product price"
          value={newProductPrice}
          onChange={(e) => setNewProductPrice(parseFloat(e.target.value))}
          style={{ marginRight: '1rem' }}
        />
        <button onClick={createProduct}>Add Product</button>
      </div>

      <ul>
        {products.map((product) => (
          <li key={product.id}>
            {product.name} - ${product.price.toFixed(2)}
          </li>
        ))}
      </ul>
    </div>
  );
}