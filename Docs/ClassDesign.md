# Card Dash

## Class

// only for game logic, none for appearance

### GameManager

enum State { Decision, Judge, Settle }; // 抉择 判定 结算

State currentState; // 当前阶段

enum Round { Speed, Attack }; // 速度 攻击

Round currentRound; // 当前轮次

int day; // 当前天数

List<Character> players; // 所有玩家

### Character

int health; //当前生命值

int driveAbility; //驾驶能力

int punch; //近战能力

int money; //当前金钱数

int criminal; // 通缉值

bool turnover; //是否扑街中

Car myCar; //车辆信息

List<Weapon> myWeapons; // 0 or more 武器信息

List<Character> canAttack; // 0 or more 本轮可攻击对象

enum Decision { Accelerate, Decelerate }; // 加速 刹车

Decision currentDecision; // 本回合抉择 如扑街中则无法抉择

### Car

int speed; //当前速度

Engine myEngine; // 0 or 1 引擎配置

Tyre myTyre; // 0 or 1 轮胎配置

Map myMap; // 所在地图

int mileage; // 里程数

### Engine

int driveEase; // 容易驾驶

int price; // 定价

### Tyre

enum TyreFeature { Accelerate, Decelerate }; // 加速或刹车

TyreFeature tyreFeature; // 轮胎类型

int value; // 属性值

int price; // 定价

### Weapon

enum WeaponDistance{ Short, Long }; // 近战或远程

WeaponDistance weaponDistance; // 武器类型

int power; // 攻击力

int criminal; // 通缉值

int price; // 定价

### Attack

Character from; // 发起方

Character to; // 接受方

Weapon weapon; // 0 or 1 无武器代表拳击 根据武器类型判断近战或远程

### Map

int driveDifficulty; // 驾驶难度

int length; // 长度

bool flipped; // 是否已知

List<Map> nextMap; // 0 or more, one-way link 紧接着的地图