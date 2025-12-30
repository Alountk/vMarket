import { test, expect } from '@playwright/test';

test.beforeAll(async ({ request }) => {
  const response = await request.post('http://localhost:5017/api/Users', {
    data: {
      firstName: 'John',
      lastName: 'Doe',
      email: 'test@example.com',
      password: 'StrongPassword123!',
      address: '123 Test St',
      city: 'Test City',
      country: 'Test Country',
      phone: '+1234567890'
    }
  });
  if (response.status() === 400) {
    const errorBody = await response.json();
    console.log('Registration 400 details:', JSON.stringify(errorBody, null, 2));
  }
});

test.describe('Authentication Flow', () => {
  test('should login successfully with valid credentials', async ({ page }) => {
    // Go to homepage
    await page.goto('/', { waitUntil: 'networkidle' });
    
    // Click on Sign in link (top bar)
    await page.click('text=Sign in');
    
    // Verify we are on login page
    await expect(page).toHaveURL(/.*login/);
    
    // Fill login form
    await page.getByLabel('Email Address').fill('test@example.com');
    await page.getByLabel('Password').fill('StrongPassword123!');
    
    // Click sign in button
    await page.getByRole('button', { name: 'Sign In' }).click();
    
    // Verify we are redirected to home
    await expect(page).toHaveURL('http://localhost:3000/');
    
    // Verify user greeting in Navbar
    await expect(page.locator('text=Hi John!')).toBeVisible();
  });

  test('should logout successfully', async ({ page }) => {
    // Login first
    await page.goto('/login');
    await page.getByLabel('Email Address').fill('test@example.com');
    await page.getByLabel('Password').fill('StrongPassword123!');
    await page.getByRole('button', { name: 'Sign In' }).click();
    
    // Verify logged in
    await expect(page.locator('text=Hi John!')).toBeVisible();
    
    // Click Sign out
    await page.getByRole('button', { name: 'Sign out' }).click();
    
    // Verify Sign in is visible again
    await expect(page.getByRole('link', { name: 'Sign in' })).toBeVisible();
    await expect(page.locator('text=Hi John!')).not.toBeVisible();
  });

  test('should show error with invalid credentials', async ({ page }) => {
    await page.goto('/login');
    await page.getByLabel('Email Address').fill('wrong@example.com');
    await page.getByLabel('Password').fill('WrongPassword123!');
    await page.getByRole('button', { name: 'Sign In' }).click();
    
    // Verify error message
    await expect(page.locator('text=Invalid credentials')).toBeVisible();
  });
});
