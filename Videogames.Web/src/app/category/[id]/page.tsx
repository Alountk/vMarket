"use client";

import { useEffect, useState, useCallback, use } from "react";
import { Squares2X2Icon, ListBulletIcon } from "@heroicons/react/24/outline";
import Link from "next/link";
import { CATEGORIES } from "../../../constants/categories";
import { Videogame } from "../../../domain/models/Videogame";
import { VideogameService } from "../../../infrastructure/services/VideogameService";
import { useAuth } from "../../../context/AuthContext";
import VideogameCard from "../../../components/VideogameCard";

export default function CategoryPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = use(params);
  const [videogames, setVideogames] = useState<Videogame[]>([]);
  const [loading, setLoading] = useState(true);
  const { isAuthenticated } = useAuth();
  const [videogameService] = useState(() => new VideogameService());

  const category = CATEGORIES.find((c) => c.id === id);

  const loadVideogames = useCallback(async () => {
    if (!category) return;
    try {
      setLoading(true);
      const allGames = await videogameService.getAll();
      // Filter by categoryId (mapping categorical ID to back-end int)
      const filtered = allGames.filter(
        (g) => g.category === category.categoryId
      );
      setVideogames(filtered);
    } catch (error: unknown) {
      console.error("Failed to load videogames for category", error);
    } finally {
      setLoading(false);
    }
  }, [videogameService, category]);

  useEffect(() => {
    loadVideogames();
  }, [loadVideogames]);

  const handleDelete = useCallback(
    async (gameId: string) => {
      if (confirm("Are you sure you want to delete this listing?")) {
        try {
          await videogameService.delete(gameId);
          loadVideogames();
        } catch (error) {
          console.error("Failed to delete videogame", error);
        }
      }
    },
    [loadVideogames, videogameService]
  );

  if (!category) {
    return (
      <div className="max-w-7xl mx-auto px-4 py-16 text-center">
        <h1 className="text-2xl font-bold mb-4">Category not found</h1>
        <Link href="/" className="text-blue-600 hover:underline">
          Return Home
        </Link>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-4 py-8 bg-white dark:bg-gray-900 transition-colors duration-300">
      {/* Breadcrumbs */}
      <nav className="flex items-center gap-2 text-sm text-gray-500 dark:text-gray-400 mb-8">
        <Link href="/" className="hover:text-blue-600">
          Home
        </Link>
        <span>/</span>
        <span className="font-bold text-gray-900 dark:text-white uppercase tracking-wider">
          {category.name}
        </span>
      </nav>

      <div className="flex flex-col md:flex-row gap-8">
        {/* Sidebar - eBay Style Filters */}
        <aside className="w-full md:w-64 shrink-0">
          <div className="sticky top-24">
            <h2 className="text-lg font-bold text-gray-900 dark:text-white mb-4 border-b pb-2">
              {category.name}
            </h2>
            <div className="mb-8">
              <h3 className="text-sm font-bold text-gray-700 dark:text-gray-300 mb-3 uppercase tracking-tight">
                Categories
              </h3>
              <ul className="space-y-2">
                {category.subcategories.map((sub) => (
                  <li key={sub}>
                    <button className="text-sm text-gray-600 dark:text-gray-400 hover:text-blue-600 dark:hover:text-blue-400 block w-full text-left">
                      {sub}
                    </button>
                  </li>
                ))}
              </ul>
            </div>

            <div className="mb-8">
              <h3 className="text-sm font-bold text-gray-700 dark:text-gray-300 mb-3 uppercase tracking-tight">
                Condition
              </h3>
              <div className="space-y-2">
                {["New", "Like New", "Very Good", "Good", "Acceptable"].map(
                  (cond) => (
                    <label
                      key={cond}
                      className="flex items-center gap-2 text-sm text-gray-600 dark:text-gray-400 cursor-pointer hover:text-blue-600"
                    >
                      <input
                        type="checkbox"
                        className="rounded border-gray-300 text-blue-600 focus:ring-blue-500"
                      />
                      {cond}
                    </label>
                  )
                )}
              </div>
            </div>

            <div className="mb-8">
              <h3 className="text-sm font-bold text-gray-700 dark:text-gray-300 mb-3 uppercase tracking-tight">
                Price Range
              </h3>
              <div className="flex items-center gap-2">
                <input
                  type="text"
                  placeholder="Min"
                  className="w-full px-2 py-1 text-sm border rounded dark:bg-gray-800 dark:border-gray-700"
                />
                <span className="text-gray-400">-</span>
                <input
                  type="text"
                  placeholder="Max"
                  className="w-full px-2 py-1 text-sm border rounded dark:bg-gray-800 dark:border-gray-700"
                />
              </div>
            </div>
          </div>
        </aside>

        {/* Main Content */}
        <main className="flex-1">
          {/* Header & Controls */}
          <div className="bg-white dark:bg-gray-800 p-4 rounded-xl border border-gray-200 dark:border-gray-700 mb-8 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
            <div>
              <h1 className="text-2xl font-bold text-gray-900 dark:text-white">
                {category.name}
              </h1>
              <p className="text-sm text-gray-500 dark:text-gray-400">
                {videogames.length} listings found
              </p>
            </div>
            <div className="flex items-center gap-4">
              <div className="flex items-center bg-gray-100 dark:bg-gray-700 rounded-lg p-1">
                <button className="p-1.5 rounded-md bg-white dark:bg-gray-600 shadow-sm text-blue-600">
                  <Squares2X2Icon className="h-5 w-5" />
                </button>
                <button className="p-1.5 rounded-md text-gray-500 dark:text-gray-400">
                  <ListBulletIcon className="h-5 w-5" />
                </button>
              </div>
              <select className="bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg px-3 py-1.5 text-sm font-medium focus:ring-2 focus:ring-blue-500 outline-none">
                <option>Best Match</option>
                <option>Price: Low to High</option>
                <option>Price: High to Low</option>
                <option>Newly Listed</option>
              </select>
            </div>
          </div>

          {/* Grid */}
          {loading ? (
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
              {[1, 2, 3, 4, 5, 6, 7, 8].map((i) => (
                <div
                  key={i}
                  className="aspect-3/4 bg-gray-100 dark:bg-gray-800 rounded-xl animate-pulse border border-gray-100 dark:border-gray-700"
                ></div>
              ))}
            </div>
          ) : videogames.length > 0 ? (
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
              {videogames.map((game) => (
                <VideogameCard
                  key={game.id}
                  videogame={game}
                  isAuthenticated={isAuthenticated}
                  onDelete={handleDelete}
                />
              ))}
            </div>
          ) : (
            <div className="py-24 text-center bg-gray-50 dark:bg-gray-800/50 rounded-2xl border border-dashed border-gray-300 dark:border-gray-700">
              <div className="max-w-sm mx-auto">
                <div className="bg-blue-50 dark:bg-blue-900/20 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                  <Squares2X2Icon className="h-8 w-8 text-blue-600" />
                </div>
                <h3 className="text-lg font-bold text-gray-900 dark:text-white mb-2">
                  No items found
                </h3>
                <p className="text-gray-500 dark:text-gray-400 mb-6">
                  There are currently no items listed in this category.
                </p>
                <Link
                  href="/create"
                  className="inline-flex items-center gap-2 px-6 py-2 bg-blue-600 hover:bg-blue-700 text-white font-bold rounded-full transition-all"
                >
                  Post your listing
                </Link>
              </div>
            </div>
          )}
        </main>
      </div>
    </div>
  );
}
