import { Routes, Route } from 'react-router-dom';
import CreateGamePage from '../pages/CreateGamePage';
import GameListPage from '../pages/GameListPage';
import GameSessionPage from '../pages/GameSessionPage';
import GameScorePage from '../pages/GameScorePage';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<GameListPage />} />
      <Route path="/create" element={<CreateGamePage />} />
      <Route path="/session/:sessionId" element={<GameSessionPage />} />
      <Route path="/session/:sessionId/result" element={<GameScorePage />} />
    </Routes>
  );
};

export default AppRoutes;
