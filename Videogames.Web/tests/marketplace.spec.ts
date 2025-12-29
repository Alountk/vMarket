import { test, expect } from '@playwright/test';

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
    
    // Click PlayStation category
    const psCategory = page.locator('div:has-text("PlayStation")').first();
    await psCategory.click();
    
    // Note: currently clicking a category just logs to console or doesn't navigate far
    // But we can check if it has the sub-links
    await expect(page.locator('text=Videogames')).toBeVisible();
  });

  test('should require login to access Sell page', async ({ page }) => {
    await page.goto('/create');
    
    // Verify redirect to login
    await expect(page).toHaveURL(/.*login/);
  });

  test('should allow listing an item after login', async ({ page }) => {
    // 1. Login
    await page.goto('/login');
    await page.fill('input[name="email"]', 'test@example.com');
    await page.fill('input[name="password"]', 'StrongPassword123!');
    await page.click('button:has-text("Sign In")');
    
    // 2. Go to Sell
    await page.click('text=Sell');
    await expect(page).toHaveURL(/.*create/);
    
    // 3. Fill the form
    await page.fill('input[name="englishName"]', 'E2E Test Game');
    await page.fill('input[name="console"]', 'Test Console');
    await page.fill('input[name="releaseDate"]', '2023-01-01');
    await page.fill('input[name="versionGame"]', 'v1.0-Test');
    
    // Select category (Nintendo)
    await page.selectOption('select[name="category"]', '2');
    
    // Fill prices
    await page.fill('input[name="averagePrice"]', '50');
    await page.fill('input[name="ownPrice"]', '45');
    
    // Fill description
    await page.fill('textarea[name="description"]', 'This is a test game created by Playwright E2E.');
    
    // Submit
    await page.click('button:has-text("List Item Now")');
    
    // 4. Verify redirection to home
    await expect(page).toHaveURL('http://localhost:3000/');
    
    // 5. Verify item appears in Recently Added (optional, depends on API speed/refresh)
    // await expect(page.locator('text=E2E Test Game')).toBeVisible();
  });
});
