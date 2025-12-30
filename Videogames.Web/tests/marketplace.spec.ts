import { test, expect } from '@playwright/test';

test.beforeAll(async ({ request }) => {
  // Try to register the test user
  await request.post('http://localhost:5017/api/Users', {
    data: {
      firstName: 'John',
      lastName: 'Doe',
      email: 'test@example.com',
      password: 'StrongPassword123!'
    }
  });
});

test.describe('Marketplace Flow', () => {
  test('should display hero section and categories on home page', async ({ page }) => {
    await page.goto('/');
    
    // Verify hero text
    await expect(page.locator('text=The Ultimate Marketplace for Gamers')).toBeVisible();
    
    // Verify some categories
    await expect(page.locator('text=PlayStation')).toBeVisible();
    await expect(page.locator('text=Nintendo')).toBeVisible();
    await expect(page.locator('text=Xbox')).toBeVisible();
  });

  test('should navigate to category and show subcategories', async ({ page }) => {
    await page.goto('/');
    
    // Click PlayStation category link directly
    await page.click('a[href="/category/ps"]');
    
    // Verify redirection to category page
    await expect(page).toHaveURL(/\/category\/ps/);
    
    // Check if the main heading is PlayStation
    await expect(page.getByRole('heading', { level: 1, name: 'PlayStation' })).toBeVisible();
    
    // Verify subcategories are visible in the sidebar
    await expect(page.locator('aside')).toContainText('Videogames');
  });

  test('should require login to access Sell page', async ({ page }) => {
    await page.goto('/create');
    
    // Verify redirect to login
    await expect(page).toHaveURL(/.*login/);
  });

  test('should allow listing an item after login', async ({ page }) => {
    // 1. Login
    await page.goto('/login');
    await page.getByLabel('Email Address').fill('test@example.com');
    await page.getByLabel('Password').fill('StrongPassword123!');
    await page.getByRole('button', { name: 'Sign In' }).click();
    
    // Wait for the URL to change back to home and user greeting to appear
    await expect(page).toHaveURL(/.*\//);
    await expect(page.locator('text=Hi John!')).toBeVisible();
    
    // Ensure localstorage is synced before moving to the next page
    await page.waitForFunction(() => localStorage.getItem('user') !== null);
    
    // 2. Go to Sell
    // Using click to maintain session context better than hard navigation
    await page.getByRole('link', { name: 'Sell', exact: true }).click();
    await page.waitForLoadState('networkidle');
    await expect(page).toHaveURL(/.*create/);
    
    // 3. Fill the form
    await page.getByLabel('English Name').fill('E2E Test Game');
    await page.getByLabel('Console').fill('Test Console');
    await page.getByLabel('Release Date').fill('2023-01-01');
    await page.getByLabel('Version').fill('v1.0-Test');
    
    // Select category (Nintendo is value 2)
    await page.getByLabel('Category').selectOption('2');
    
    // Fill prices
    await page.getByLabel('Average Market Price').fill('50');
    await page.getByLabel('Your Asking Price').fill('45');
    
    // Fill description
    await page.getByLabel('Detailed Description').fill('This is a test game created by Playwright E2E.');
    
    // Submit
    await page.getByRole('button', { name: 'List Item Now' }).click();
    
    // 4. Verify redirection to home
    await expect(page).toHaveURL('http://localhost:3000/');
  });
});
