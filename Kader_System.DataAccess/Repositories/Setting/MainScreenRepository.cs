﻿namespace Kader_System.DataAccess.Repositories.Setting;

public class MainScreenRepository(KaderDbContext context) : BaseRepository<StMainScreenCat>(context), IMainScreenRepository
{
}
