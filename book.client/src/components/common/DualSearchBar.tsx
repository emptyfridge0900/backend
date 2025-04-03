import React from 'react';

interface DualSearchBarProps {
  authorSearch: string;
  titleSearch: string;
  setAuthorSearch: (term: string) => void;
  setTitleSearch: (term: string) => void;
  onSearch: (author: string, title: string) => void;
}

export const DualSearchBar: React.FC<DualSearchBarProps> = ({
  authorSearch,
  titleSearch,
  setAuthorSearch,
  setTitleSearch,
  onSearch,
}) => {
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch(authorSearch, titleSearch);
  };

  const handleClear = () => {
    setAuthorSearch('');
    setTitleSearch('');
    onSearch('', '');
  };

  return (
    <form onSubmit={handleSubmit} className="w-full max-w-[800px] flex flex-col sm:flex-row gap-4">
      <div className="flex-1 flex gap-2">
        <input
          type="text"
          value={titleSearch}
          onChange={(e) => setTitleSearch(e.target.value)}
          placeholder="Search by title"
          className="w-full p-2.5 border border-gray-300 rounded text-base transition-border duration-200 focus:border-blue-600 focus:shadow-[0_0_5px_rgba(0,102,204,0.2)]"
        />
      </div>
      <div className="flex-1 flex gap-2">
        <input
          type="text"
          value={authorSearch}
          onChange={(e) => setAuthorSearch(e.target.value)}
          placeholder="Search by author"
          className="w-full p-2.5 border border-gray-300 rounded text-base transition-border duration-200 focus:border-blue-600 focus:shadow-[0_0_5px_rgba(0,102,204,0.2)]"
        />
      </div>
      
      <button
        type="submit"
        className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition-colors"
      >
        Search
      </button>
      <button type="button" onClick={handleClear} className="px-4 py-2 bg-gray-500 text-white rounded hover:bg-gray-600 transition-colors">
        Clear
      </button>
    </form>
  );
};

export default DualSearchBar;