using DapperRepository;
using DataAccessLayer;

//Connection
string connection = @"Server=(LocalDB)\MSSQLLocalDB;Integrated security=SSPI;database=CategoryProductDapper";
GenericUnitOfWork work = new GenericUnitOfWork(connection);

var reposCategory = work.Repository<Category>();
var reposProduct = work.Repository<Product>();
//------- INSERT
//reposCategory.Add(new Category { Name = "First" });
//reposCategory.Add(new Category { Name = "Second" });
//
//reposProduct.Add(new Product { Title = "First", Price = 2998 });
//reposProduct.Add(new Product { Title = "Second", Price = 8992, Description = "Some description for second product", CategoryID = 2 });
//------- SELECT
//var category = reposCategory.GetAll();
//foreach (var item in category)
//    Console.WriteLine($"ID: {item.ID} | Name: {item.Name}");
//Console.WriteLine();
//
//var product = reposProduct.GetAll();
//foreach (var item in product)
//    Console.WriteLine($"ID: {item.ID} | Title: {item.Title} | Price: {item.Price} | Description: {item.Description} | CategoryID: {item.CategoryID}");
//------ UPDATE
//var category = reposCategory.FindById(1);
//category.Name = "Hello world!";
//reposCategory.Update(category);
//
//var product = reposProduct.FindById(1);
//product.Price = 1998;
//reposProduct.Update(product);
//
//var category2 = reposCategory.GetAll();
//foreach (var item in category2)
//    Console.WriteLine($"ID: {item.ID} | Name: {item.Name}");
//Console.WriteLine();
//
//var product2 = reposProduct.GetAll();
//foreach (var item in product2)
//    Console.WriteLine($"ID: {item.ID} | Title: {item.Title} | Price: {item.Price} | Description: {item.Description} | CategoryID: {item.CategoryID}");
//------- DELETE
//reposCategory.Remove(1);
//
//reposProduct.Remove(2);
//
//var category = reposCategory.GetAll();
//foreach (var item in category)
//    Console.WriteLine($"ID: {item.ID} | Name: {item.Name}");
//Console.WriteLine();
//
//var product = reposProduct.GetAll();
//foreach (var item in product)
//    Console.WriteLine($"ID: {item.ID} | Title: {item.Title} | Price: {item.Price} | Description: {item.Description} | CategoryID: {item.CategoryID}");