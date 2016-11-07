# .NET Core Boilerplate

> note: alpha stages, adding webpack and reactjs.net next

# How to use

1. clone to your machine
- `npm install -g now yarn`
- `yarn && dotnet restore` -or- `npm run setup`
- if using Entity Framework / a database:

    - modify Models/*.cs to create your csharp Models for Entity Framework Core; add any seeded data to the `Seeder` class
    - `dotnet ef migrations add init` - create the initial migrations for the database seeding
    - `dotnet ef database update` - write the migrations to the database
    - if at any point you change a model, rerun the preceding steps

- `npm start` - runs and watch the files for changes. Underneath, this runs `dotnet watch run`, `npm run css:watch`, `npm run js:watch` for CSS and JS build tools.
- if at any point you install a package through NuGet or npm, or change the project.json or package.json files - hit Ctrl+C and run `npm run setup` again.
- open `http://localhost:5000` to view local server

# To deploy

To https://now.sh: `npm run deploy`

# Support

1. Please submit issues on GitHub with proper taggings / labels.
2. Reach out to [@matthiasak](https://twitter.com/matthiasak).
