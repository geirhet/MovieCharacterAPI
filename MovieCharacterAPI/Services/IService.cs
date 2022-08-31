using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieCharacterAPI.Services
{
	public interface IService<T>
	{
		/// <summary>
		/// Adds entity to the current table context before saving the changes and returns the entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>Returns the entity object.</returns>
		Task<T> Add(T entity);
		/// <summary>
		/// Checks if the id passed in exists for the current table context.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>True if entity exists, otherwise false.</returns>
		bool Exists(int id);
		/// <summary>
		/// Deletes the entity with the corresponding Id in the corresponding context table.
		/// </summary>
		/// <param name="id"></param>
		Task Delete(int id);
		/// <summary>
		/// Gets all entities in the corresponding table and returns the list.
		/// </summary>
		/// <returns>IEnumerable list of entities.</returns>
		Task<IEnumerable<T>> GetAll();
		/// <summary>
		/// Gets entity by Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns the entity object if it exists, otherwise null.</returns>
		Task<T> GetById(int id);
		/// <summary>
		/// Updates entity with the new entity object.
		/// </summary>
		/// <param name="entity"></param>
		Task Update(T entity);
	}
}


