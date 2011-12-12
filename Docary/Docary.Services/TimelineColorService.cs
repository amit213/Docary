using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;
using Docary.Repositories;

namespace Docary.Services
{
    public class TimelineColorService : ITimelineColorService
    {
        public ITimelineColorRepository _timelineColorRepository;

        public TimelineColorService(ITimelineColorRepository timelineColorRepository)
        {
            _timelineColorRepository = timelineColorRepository;
        }

        public TimelineColor GetRandom()
        {
            var uniqueIds = _timelineColorRepository.Get().Select(tc => tc.Id).OrderBy(tc => tc).ToArray();
            
            var random = new Random().Next(0, uniqueIds.Length);
            var randomId = uniqueIds[random];

            return _timelineColorRepository.Get().Where(tc => tc.Id == randomId).First();
        }
    }
}
