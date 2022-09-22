### Docker:
  Для поднятия бд в контейнере:
````
docker compose up -d
````
  
### Backend
C# + ASP.Net core

Для миграции бд нужно запустить сборку с аргументом командной строки "migrate".

#### Проекты:

- Circles.Application - содержит сервисы для работы с репозиториями Users и Circles
- Circles.Auth - отвечает за аутентификацию
- Circles.Domain - содержит абстрации репозиториев и сущности для всего приложения
- Circles.Persistence - уровень работы с бд

Все методы контроллера(кроме Login) работают с проверкой jwt токана.
Все пароли сохраняются в бд по такому алгоритму: md5(md5("пароль") + "соль").
Соль, ключи и тд храню в user-secrets.

Для изменения стоки коннекта к бд:
````
dotnet user-secrets set "ConnectionString" "[новая строка]"
````

### Frontend
Vue.js 3 + TypeScript + Vuex + axios

Логин/пароль: 1/1 или admin/admin

На клиенте храню все круги с id, x, y. Обновляю их при каждом логине или валидации.
Описание получаю и меняю отдельными запросами.
При коде ошибки 401 всегда возвращаю пользователя на страницу авторизации.

### Для ручного создания таблиц:

````
CREATE TABLE circles
(
    id SERIAL PRIMARY KEY,
    description text NOT NULL,
    x bigint NOT NULL,
    y bigint NOT NULL
);
CREATE TABLE users
(
    login text PRIMARY KEY,
    password text
);
CREATE INDEX idx_id ON circles
(
    id
);
CREATE INDEX idx_login_id ON users
(
    login
);
INSERT INTO circles (description, x, y)
VALUES
  ('Окружность 1', 0, 10),
  ('Окружность 2', 100, 100),
  ('Окружность 3', 150, 100),
  ('Окружность 4', 33, 89),
  ('Окружность 5', 120, 400),
  ('Окружность 6', 18, 3),
  ('Окружность 7', 2500, 3020);
  INSERT INTO users (login, password)
VALUES
  ('admin', 'E37C0A61C2ABBFE419367F572BA7AA11'),
  ('1', 'C372F08B3D96DCBD80576B24E8B03BB9')
````
