import React from 'react';

interface PaginationProps {
  currentPage: number;
  totalRecords: number; // Never undefined due to useBooks initialization
  itemsPerPage: number;
  onPageChange: (page: number) => void;
}

export const Pagination: React.FC<PaginationProps> = ({
  currentPage,
  totalRecords,
  itemsPerPage,
  onPageChange,
}) => {
  const totalPages = Math.ceil(totalRecords / itemsPerPage);
  const pageNumbers = Array.from({ length: totalPages }, (_, i) => i + 1);
  console.log(pageNumbers)
  return (
    <div className="flex justify-center gap-1.25 p-5">
      {pageNumbers.map((page) => (
        <button
          key={page}
          onClick={() => onPageChange(page-1)}
          className={`px-3 py-2 border border-gray-300 bg-white cursor-pointer rounded transition-all duration-200 hover:bg-gray-200 ${
            currentPage === page-1 ? 'bg-blue-600 text-white border-blue-600' : ''
          }`}
        >
          {page}
        </button>
      ))}
    </div>
  );
};

export default Pagination;